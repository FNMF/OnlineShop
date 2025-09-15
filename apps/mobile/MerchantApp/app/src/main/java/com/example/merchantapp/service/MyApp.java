package com.example.merchantapp.service;

import android.app.Application;
import android.content.Intent;
import android.view.PixelCopy;

public class MyApp extends Application {
    @Override
    public void onCreate() {
        super.onCreate();
        // 这里是全局初始化的地方

        String savedToken = getSharedPreferences("app", MODE_PRIVATE)
                .getString("jwt", null);
        UserManager.getInstance().saveToken(savedToken);

        // 初始化 Retrofit
        ApiClient.init("https://api.example.com/"); // TODO，改为后端地址

        // 启动 SignalR 前台服务
        Intent serviceIntent = new Intent(this, SignalRService.class);
        startService(serviceIntent);

        // 例如：初始化对象管理
        UserManager.init(this);
        ProductManager.init(this);


        // 例如：初始化网络库、版本检测
        // NetworkClient.init();
        // VersionChecker.checkUpdate();
    }


}
