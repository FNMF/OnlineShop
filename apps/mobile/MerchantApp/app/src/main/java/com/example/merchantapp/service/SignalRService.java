package com.example.merchantapp.service;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.Service;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Build;
import android.os.IBinder;
import android.util.Log;

import androidx.annotation.Nullable;
import androidx.core.app.NotificationCompat;

import com.example.merchantapp.R;
import com.microsoft.signalr.HubConnection;
import com.microsoft.signalr.HubConnectionBuilder;

import java.util.concurrent.Executors;

public class SignalRService extends Service {
    private static final String TAG = "SignalRService";
    private static final String CHANNEL_ID = "signalr_channel";
    private HubConnection hubConnection;

    @Override
    public void onCreate() {
        super.onCreate();

        // 1. 创建通知渠道
        createNotificationChannel();

        // 2. 构建通知
        Notification notification = new NotificationCompat.Builder(this, CHANNEL_ID)
                .setContentTitle("实时连接已开启")
                .setContentText("应用正在接收实时消息")
                .setSmallIcon(R.drawable.ic_launcher_foreground)
                .setPriority(NotificationCompat.PRIORITY_LOW)
                .build();

        // 3. 启动前台服务
        startForeground(1, notification);

        // 4. 初始化 SignalR
        hubConnection = HubConnectionBuilder.create("https://your-api.com/hub")//todo，改为后端api
                .build();

        // 注册消息接收
        hubConnection.on("ReceiveMessage", (user, message) -> {
            Log.d(TAG, "收到消息: " + user + ": " + message);
            // 可以通过广播、LiveData、EventBus 通知 UI
        }, String.class, String.class);

        // 5. 异步启动连接，避免阻塞主线程
        Executors.newSingleThreadExecutor().execute(() -> {
            try {
                Log.d(TAG, "开始连接 SignalR...");
                hubConnection.start().blockingAwait();
                Log.d(TAG, "SignalR 连接成功");
            } catch (Exception e) {
                Log.e(TAG, "SignalR 连接失败", e);
                // 不要让整个 Service 崩溃，可以考虑重试
                scheduleReconnect();
            }
        });
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        return START_STICKY;
    }

    @Override
    public void onDestroy() {
        if (hubConnection != null) {
            hubConnection.stop();
        }
        super.onDestroy();
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    private void createNotificationChannel() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel serviceChannel = new NotificationChannel(
                    CHANNEL_ID,
                    "SignalR 实时服务",
                    NotificationManager.IMPORTANCE_LOW
            );
            NotificationManager manager = getSystemService(NotificationManager.class);
            if (manager != null) {
                manager.createNotificationChannel(serviceChannel);
            }
        }
    }

    private boolean isNetworkAvailable() {
        ConnectivityManager cm = (ConnectivityManager) getSystemService(CONNECTIVITY_SERVICE);
        if (cm != null) {
            NetworkInfo activeNetwork = cm.getActiveNetworkInfo();
            return activeNetwork != null && activeNetwork.isConnected();
        }
        return false;
    }


    /**
     * 失败后尝试 5 秒后重连
     */
    private void scheduleReconnect() {
        Executors.newSingleThreadScheduledExecutor()
                .schedule(() -> {
                    if (!isNetworkAvailable()) {
                        Log.w(TAG, "网络不可用，延迟重试");
                        scheduleReconnect();
                        return;
                    }
                    try {
                        Log.d(TAG, "尝试重新连接 SignalR...");
                        hubConnection.start().blockingAwait();
                        Log.d(TAG, "重连成功");
                    } catch (Exception e) {
                        Log.e(TAG, "重连失败", e);
                        scheduleReconnect(); // 继续重试
                    }
                }, 5, java.util.concurrent.TimeUnit.SECONDS);
    }

}
