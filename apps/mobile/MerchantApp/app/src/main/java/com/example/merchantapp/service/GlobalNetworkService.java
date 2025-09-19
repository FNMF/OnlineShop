package com.example.merchantapp.service;

import android.app.Activity;
import android.app.Application;
import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.Network;
import android.os.Build;
import android.os.Handler;
import android.os.Looper;

import androidx.core.app.NotificationCompat;

import com.example.merchantapp.R;
import com.microsoft.signalr.HubConnection;
import com.microsoft.signalr.HubConnectionBuilder;

public class GlobalNetworkService implements Application.ActivityLifecycleCallbacks {

    private static GlobalNetworkService instance;
    private final Context context;
    private HubConnection hubConnection;
    private boolean dialogShowing = false;

    private GlobalNetworkService(Context context) {
        this.context = context.getApplicationContext();
        initSignalR();
        registerNetworkCallback();
    }

    public static void init(Application app) {
        if (instance == null) {
            instance = new GlobalNetworkService(app);
            app.registerActivityLifecycleCallbacks(instance);
        }
    }

    // ------------------- SignalR -------------------
    private static final String CHANNEL_ID = "signalr_channel";

    private void initSignalR() {
        createNotificationChannel();

        Notification notification = new NotificationCompat.Builder(context, CHANNEL_ID)
                .setContentTitle("实时连接已开启")
                .setContentText("应用正在接收实时消息")
                .setSmallIcon(R.drawable.ic_launcher_foreground)
                .setPriority(NotificationCompat.PRIORITY_LOW)
                .build();

        Intent serviceIntent = new Intent(context, SignalRForegroundService.class);
        context.startService(serviceIntent);

        // 初始化 HubConnection
        hubConnection = HubConnectionBuilder.create("https://your-api.com/hub")//TODO，NotificationHub的API
                .build();

        hubConnection.on("ReceiveMessage", (user, message) -> {
            // TODO: 推送给 UI 或存储
        }, String.class, String.class);

        new Thread(() -> {
            try {
                hubConnection.start().blockingAwait();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }).start();
    }

    private void createNotificationChannel() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel serviceChannel = new NotificationChannel(
                    CHANNEL_ID,
                    "SignalR 实时服务",
                    NotificationManager.IMPORTANCE_LOW
            );
            NotificationManager manager = context.getSystemService(NotificationManager.class);
            if (manager != null) manager.createNotificationChannel(serviceChannel);
        }
    }

    // ------------------- 网络检测 -------------------
    private void registerNetworkCallback() {
        ConnectivityManager cm = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        if (cm == null) return;

        cm.registerDefaultNetworkCallback(new ConnectivityManager.NetworkCallback() {
            @Override
            public void onAvailable(Network network) {
                checkBackend();
            }

            @Override
            public void onLost(Network network) {
                showDialog(true, null);
            }
        });
    }

    private void checkBackend() {
        NetworkUtils.checkBackend(context, new NetworkUtils.BackendCheckCallback() {
            @Override
            public void onBackendAvailable(String message) {
                // 后端可用，不弹窗
            }

            @Override
            public void onBackendError(String message) {
                showDialog(false, message);
            }
        });
    }

    // ------------------- 弹窗 -------------------
    private void showDialog(boolean noNetwork, String serverMessage) {
        if (dialogShowing) return;

        Activity currentActivity = ActivityTracker.getCurrentActivity();
        if (currentActivity == null) return;

        dialogShowing = true;
        new Handler(Looper.getMainLooper()).post(() -> {
            new androidx.appcompat.app.AlertDialog.Builder(currentActivity)
                    .setTitle("网络错误")
                    .setMessage(noNetwork ? "请检查网络后重试" : serverMessage)
                    .setPositiveButton("重试", (dialog, which) -> {
                        dialog.dismiss();
                        dialogShowing = false;
                        if (noNetwork) {
                            checkBackend();
                        } else {
                            checkBackend();
                        }
                    })
                    .setNegativeButton("退出", (dialog, which) -> {
                        dialog.dismiss();
                        dialogShowing = false;
                        currentActivity.finishAffinity();
                    })
                    .setCancelable(false)
                    .show();
        });
    }

    // ------------------- Activity 生命周期跟踪 -------------------
    @Override
    public void onActivityCreated(Activity activity, android.os.Bundle savedInstanceState) { }
    @Override
    public void onActivityStarted(Activity activity) { ActivityTracker.setCurrentActivity(activity); }
    @Override
    public void onActivityResumed(Activity activity) { ActivityTracker.setCurrentActivity(activity); }
    @Override
    public void onActivityPaused(Activity activity) { }
    @Override
    public void onActivityStopped(Activity activity) { }
    @Override
    public void onActivitySaveInstanceState(Activity activity, android.os.Bundle outState) { }
    @Override
    public void onActivityDestroyed(Activity activity) { }
}
