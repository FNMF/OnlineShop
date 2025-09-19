package com.example.merchantapp.service;

import android.content.Context;
import android.content.SharedPreferences;

public class UserManager {
    private static SharedPreferences prefs;
    private static final String PREF_NAME = "user_prefs";
    private static final String KEY_UUID = "uuid";
    private static final String KEY_TOKEN = "jwt_token";

    public static void init(Context context) {
        if (prefs == null) {
            prefs = context.getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE);
        }
    }

    public static void saveToken(String token) {
        if (prefs != null) {
            prefs.edit().putString(KEY_TOKEN, token).apply();
        }
    }

    public static String getToken() {
        return prefs != null ? prefs.getString(KEY_TOKEN, null) : null;
    }

    public static void saveUUID(String uuid) {
        if (prefs != null) {
            prefs.edit().putString(KEY_UUID, uuid).apply();
        }
    }

    public static String getUUID() {
        return prefs != null ? prefs.getString(KEY_UUID, null) : null;
    }

    public static boolean isLoggedIn() {
        return getToken() != null;
    }

    public static void logout() {
        if (prefs != null) {
            prefs.edit().clear().apply();
        }
    }
}
