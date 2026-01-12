package com.example.merchantapp.storage;

import android.content.Context;
import android.content.SharedPreferences;

import com.example.merchantapp.model.auth.MerchantInfo;
import com.example.merchantapp.service.MyApp;

public final class TokenManager {

    private static final Context ctx = MyApp.appContext();

    private static final String PREF = "auth_pref";
    private static final String KEY_ACCESS = "access_token";
    private static final String KEY_REFRESH = "refresh_token";
    private static final String KEY_TEMP = "temp_token";

    private TokenManager() {}

    /* ===== access ===== */

    public static void saveAccessToken(String token) {
        put(KEY_ACCESS, token);
    }

    public static String getAccessToken() {
        return get(KEY_ACCESS);
    }

    /* ===== refresh ===== */

    public static void saveRefreshToken(String token) {
        put(KEY_REFRESH, token);
    }

    public static String getRefreshToken() {
        return get(KEY_REFRESH);
    }

    /* ===== temp ===== */

    public static void saveTempToken(String token) {
        put(KEY_TEMP, token);
    }

    public static String getTempToken() {
        return get(KEY_TEMP);
    }

    /* ===== clear ===== */

    public static void clearAll() {
        ctx.getSharedPreferences(PREF, Context.MODE_PRIVATE)
                .edit().clear().apply();
    }

    /* ===== private ===== */

    private static void put(String key, String value) {
        ctx.getSharedPreferences(PREF, Context.MODE_PRIVATE)
                .edit().putString(key, value).apply();
    }

    private static String get(String key) {
        return ctx.getSharedPreferences(PREF, Context.MODE_PRIVATE)
                .getString(key, null);
    }
}





