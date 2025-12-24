package com.example.merchantapp.storage;

import android.content.Context;
import android.content.SharedPreferences;

import com.example.merchantapp.model.auth.MerchantInfo;

public class TokenManager {

    private static final String PREF = "auth_pref";

    private static final String KEY_ACCESS = "access_token";
    private static final String KEY_REFRESH = "refresh_token";
    private static final String KEY_TEMP = "temp_token";

    /* ===== 登录成功 ===== */

    public static void saveLogin(Context c,
                                 String accessToken,
                                 String refreshToken,
                                 MerchantInfo merchantInfo) {

        c.getSharedPreferences(PREF, Context.MODE_PRIVATE)
                .edit()
                .putString(KEY_ACCESS, accessToken)
                .putString(KEY_REFRESH, refreshToken)
                .putInt("account", merchantInfo.getAccount())
                .putString("phone", merchantInfo.getPhone())
                .putString("uuid",merchantInfo.getUuid())
                .remove(KEY_TEMP) // 登录后清掉临时 token
                .apply();
    }

    /* ===== 注册流程 ===== */

    public static void saveTempToken(Context c, String tempToken) {
        c.getSharedPreferences(PREF, Context.MODE_PRIVATE)
                .edit()
                .putString(KEY_TEMP, tempToken)
                .apply();
    }

    /* ===== getters ===== */

    public static String getAccessToken(Context c) {
        return c.getSharedPreferences(PREF, Context.MODE_PRIVATE)
                .getString(KEY_ACCESS, null);
    }

    public static boolean hasRefreshToken(Context c) {
        return getRefreshToken(c) != null;
    }

    public static String getRefreshToken(Context c) {
        return c.getSharedPreferences(PREF, Context.MODE_PRIVATE)
                .getString(KEY_REFRESH, null);
    }

    public static String getTempToken(Context c) {
        return c.getSharedPreferences(PREF, Context.MODE_PRIVATE)
                .getString(KEY_TEMP, null);
    }

    /* ===== 清理 ===== */

    public static void clearAll(Context c) {
        c.getSharedPreferences(PREF, Context.MODE_PRIVATE)
                .edit()
                .clear()
                .apply();
    }
}

