package com.example.merchantapp.service;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.Service;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.IBinder;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.core.app.NotificationCompat;
import androidx.core.content.ContextCompat;

import com.example.merchantapp.R;

import java.io.BufferedInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;

public class DownloadService extends Service {

    private static final String CHANNEL_ID = "download_channel";
    private static final int NOTIFICATION_ID = 1001;

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null; // 前台服务不需要绑定
    }

    @Override
    public void onCreate() {
        super.onCreate();
        createNotificationChannel();
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        String url = intent.getStringExtra("url");
        if (url == null) {
            stopSelf();
            return START_NOT_STICKY;
        }

        // Android 13+ 需要动态申请通知权限
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.TIRAMISU &&
                ContextCompat.checkSelfPermission(this, android.Manifest.permission.POST_NOTIFICATIONS)
                        != PackageManager.PERMISSION_GRANTED) {
            // 没权限，降级为 Toast 下载（无通知）
            new Thread(() -> downloadWithToast(url)).start();
        } else {
            // 有权限，前台通知下载
            startForeground(NOTIFICATION_ID, buildNotification(0));
            String filePath = getExternalFilesDir(null) + "/downloaded_file.zip";
            new Thread(() -> downloadFile(url, filePath)).start();
        }

        return START_NOT_STICKY;
    }

    private void createNotificationChannel() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel channel = new NotificationChannel(
                    CHANNEL_ID,
                    "文件下载",
                    NotificationManager.IMPORTANCE_LOW
            );
            NotificationManager manager = getSystemService(NotificationManager.class);
            if (manager != null) {
                manager.createNotificationChannel(channel);
            }
        }
    }

    private Notification buildNotification(int progress) {
        NotificationCompat.Builder builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                .setContentTitle("下载中")
                .setContentText(progress + "%")
                .setSmallIcon(R.drawable.ic_download)
                .setProgress(100, progress, false)
                .setOngoing(true);
        return builder.build();
    }

    private Notification buildCompletedNotification(String filePath) {
        return new NotificationCompat.Builder(this, CHANNEL_ID)
                .setContentTitle("下载完成")
                .setContentText("文件已保存: " + filePath)
                .setSmallIcon(R.drawable.ic_download_done)
                .setAutoCancel(true)
                .build();
    }

    private Notification buildFailedNotification() {
        return new NotificationCompat.Builder(this, CHANNEL_ID)
                .setContentTitle("下载失败")
                .setContentText("请稍后重试")
                .setSmallIcon(R.drawable.ic_download_failed)
                .setAutoCancel(true)
                .build();
    }

    private void downloadFile(String fileUrl, String filePath) {
        NotificationManager notificationManager =
                (NotificationManager) getSystemService(NOTIFICATION_SERVICE);

        try {
            URL url = new URL(fileUrl);
            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            connection.connect();

            int fileLength = connection.getContentLength();
            InputStream input = new BufferedInputStream(connection.getInputStream());
            FileOutputStream output = new FileOutputStream(filePath);

            byte[] data = new byte[4096];
            long total = 0;
            int count;
            while ((count = input.read(data)) != -1) {
                total += count;
                if (fileLength > 0) {
                    int progress = (int) (total * 100 / fileLength);
                    notificationManager.notify(NOTIFICATION_ID, buildNotification(progress));
                }
                output.write(data, 0, count);
            }

            output.flush();
            output.close();
            input.close();

            notificationManager.notify(NOTIFICATION_ID, buildCompletedNotification(filePath));
        } catch (Exception e) {
            e.printStackTrace();
            notificationManager.notify(NOTIFICATION_ID, buildFailedNotification());
        }

        stopForeground(true);
        stopSelf();
    }

    private void downloadWithToast(String fileUrl) {
        String filePath = getExternalFilesDir(null) + "/downloaded_file.zip";

        new Thread(() -> {
            try {
                URL url = new URL(fileUrl);
                HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                connection.connect();

                int fileLength = connection.getContentLength();
                InputStream input = new BufferedInputStream(connection.getInputStream());
                FileOutputStream output = new FileOutputStream(filePath);

                byte[] data = new byte[4096];
                long total = 0;
                int count;
                int lastProgress = -1;

                runOnUiThread(() -> Toast.makeText(this, "开始下载：" + fileUrl, Toast.LENGTH_SHORT).show());

                while ((count = input.read(data)) != -1) {
                    total += count;
                    if (fileLength > 0) {
                        int progress = (int) (total * 100 / fileLength);
                        if (progress % 10 == 0 && progress != lastProgress) { // 每10%提示一次
                            int finalProgress = progress;
                            runOnUiThread(() ->
                                    Toast.makeText(this, "下载进度：" + finalProgress + "%", Toast.LENGTH_SHORT).show());
                            lastProgress = progress;
                        }
                    }
                    output.write(data, 0, count);
                }

                output.flush();
                output.close();
                input.close();

                runOnUiThread(() -> Toast.makeText(this, "下载完成：" + filePath, Toast.LENGTH_LONG).show());
            } catch (Exception e) {
                e.printStackTrace();
                runOnUiThread(() -> Toast.makeText(this, "下载失败", Toast.LENGTH_SHORT).show());
            }

            stopSelf();
        }).start();
    }

    private void runOnUiThread(Runnable action) {
        new android.os.Handler(getMainLooper()).post(action);
    }
}
