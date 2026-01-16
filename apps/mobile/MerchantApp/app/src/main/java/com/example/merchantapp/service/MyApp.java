package com.example.merchantapp.service;

import android.app.Application;
import android.content.Context;

import com.example.merchantapp.BuildConfig;
import com.example.merchantapp.storage.ProductManager;
import com.example.merchantapp.storage.SessionManager;

public class MyApp extends Application {
    private static MyApp instance;
    @Override
    public void onCreate() {
        super.onCreate();
        instance = this;
        // 这里是全局初始化的地方
        // 网络检测
        //GlobalNetworkService.init(this);

        // 初始化对象管理
        SessionManager.init(this);

        // 检查应用更新
        UpdateChecker.checkForUpdate(this, BuildConfig.VERSION_CODE);
        //更新的弹窗，用于某按钮
        //UpdateChecker.showUpdateDialogIfAvailable(this, BuildConfig.VERSION_CODE);
    }
    public static Context appContext() {
        return instance.getApplicationContext();
    }

}
