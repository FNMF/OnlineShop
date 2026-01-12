package com.example.merchantapp.storage;

import android.content.Context;
import android.content.SharedPreferences;

import com.example.merchantapp.model.auth.MerchantInfo;
import com.google.gson.Gson;

import java.util.List;

public class AdminManager {
    private static final String SP_NAME = "admin_pref";
    private static final String KEY_ADMIN = "key_admin";

    public static void saveAdmin(Context context, MerchantInfo info) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        sp.edit()
                .putString(KEY_ADMIN, new Gson().toJson(info))
                .apply();
    }

    public static MerchantInfo getAdmin(Context context) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        String json = sp.getString(KEY_ADMIN, null);
        if (json == null) return null; // 如果没有存过，返回 null
        return new Gson().fromJson(json, MerchantInfo.class);
    }
    public static void clear(Context context) {
        context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE)
                .edit()
                .clear()
                .apply();
    }
}
