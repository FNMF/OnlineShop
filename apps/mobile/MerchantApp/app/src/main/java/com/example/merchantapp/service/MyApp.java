package com.example.merchantapp.service;

import android.app.Application;
import android.content.Intent;

import androidx.core.content.ContextCompat;

import com.example.merchantapp.BuildConfig;

public class MyApp extends Application {
    @Override
    public void onCreate() {
        super.onCreate();
        // 这里是全局初始化的地方
        // 网络检测
        GlobalNetworkService.init(this);

        // 例如：初始化对象管理
        UserManager.init(this);
        ProductManager.init(this);

        String savedToken = UserManager.getToken();
        if (savedToken != null) {
            UserManager.saveToken(savedToken); // 这里其实可以省略
        }

        // 初始化 Retrofit
        ApiClient.init("https://api.example.com/"); // TODO，改为后端地址

        // 检查应用更新
        UpdateChecker.checkForUpdate(this, BuildConfig.VERSION_CODE);
        //更新的弹窗，用于某按钮
        //UpdateChecker.showUpdateDialogIfAvailable(this, BuildConfig.VERSION_CODE);
    }


}
