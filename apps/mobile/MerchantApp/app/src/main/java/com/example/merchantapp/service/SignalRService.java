package com.example.merchantapp.service;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.Service;
import android.content.Intent;
import android.os.Build;
import android.os.IBinder;

import androidx.annotation.Nullable;
import androidx.core.app.NotificationCompat;

import com.example.merchantapp.R;
import com.microsoft.signalr.HubConnection;
import com.microsoft.signalr.HubConnectionBuilder;

public class SignalRService extends Service {
    private static final String CHANNEL_ID = "signalr_channel";
    private HubConnection hubConnection;

    @Override
    public void onCreate() {
        super.onCreate();

        // 1. 创建通知渠道（Android 8.0 及以上需要）
        createNotificationChannel();

        // 2. 构建通知
        Notification notification = new NotificationCompat.Builder(this, CHANNEL_ID)
                .setContentTitle("实时连接已开启")
                .setContentText("应用正在接收实时消息")
                .setSmallIcon(R.drawable.ic_launcher_foreground) // 你项目里的图标
                .setPriority(NotificationCompat.PRIORITY_LOW)
                .build();

        // 3. 启动前台服务
        startForeground(1, notification);

        // 4. 初始化 SignalR
        hubConnection = HubConnectionBuilder.create("https://your-api.com/hub")
                .build();

        hubConnection.on("ReceiveMessage", (user, message) -> {
            // 这里处理收到的消息，可以发广播或者用 LiveData 通知 UI
        }, String.class, String.class);

        hubConnection.start().blockingAwait();
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        return START_STICKY; // 如果被杀，系统会尝试重启
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
}