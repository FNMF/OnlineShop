package com.example.merchantapp.storage;

import android.content.Context;

import com.example.merchantapp.model.auth.LoginResponse;

public class SessionManager {

    private static SessionManager instance;
    private final Context appContext;

    private SessionManager(Context context) {
        this.appContext = context.getApplicationContext();
    }

    public static void init(Context context) {
        if (instance == null) {
            instance = new SessionManager(context);
        }
    }

    public static SessionManager get() {
        if (instance == null) {
            throw new IllegalStateException("SessionManager not initialized");
        }
        return instance;
    }

    public void onLoginSuccess(LoginResponse login) {
        TokenManager.saveAccessToken(login.getAccessToken());
        TokenManager.saveRefreshToken(login.getRefreshToken());
        AdminManager.saveAdmin(login.getMerchant());
    }

    public boolean isLoggedIn() {
        return TokenManager.getAccessToken() != null;
    }

    public void logout() {
        TokenManager.clearAll();
        RoleManager.clear();
        AdminManager.clear();
        ProductManager.clear();
        ShopManager.clear();
    }
}





