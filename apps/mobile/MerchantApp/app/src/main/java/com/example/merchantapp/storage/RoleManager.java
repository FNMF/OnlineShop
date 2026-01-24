package com.example.merchantapp.storage;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.util.ArrayList;
import java.util.List;

public class RoleManager {
    private static final String SP_NAME = "user_role";
    private static final String KEY_ROLES = "key_roles";
    private static Context context;
    public static void init(Context ctx) {
        context = ctx.getApplicationContext();
    }
    private static SharedPreferences sp() {
        if (context == null) {
            throw new IllegalStateException("RoleManager not initialized");
        }
        return context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
    }
    public static void saveRoles(List<String> roles) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        sp.edit()
                .putString(KEY_ROLES, new Gson().toJson(roles))
                .apply();
    }

    public static List<String> getRoles() {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        String json = sp.getString(KEY_ROLES, null);
        if (json == null) return new ArrayList<>();
        return new Gson().fromJson(json, new TypeToken<List<String>>() {}.getType());
    }

    public static boolean isShopOwner() {
        return getRoles().contains("shop_owner");
        // 与后端返回值的身份String内容相同。
    }

    public static void clear() {
        context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE)
                .edit()
                .clear()
                .apply();
    }
}
