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
    private static final String KEY_ROLES = "roles";

    public static void saveRoles(Context context, List<String> roles) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        sp.edit()
                .putString(KEY_ROLES, new Gson().toJson(roles))
                .apply();
    }

    public static List<String> getRoles(Context context) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        String json = sp.getString(KEY_ROLES, null);
        if (json == null) return new ArrayList<>();
        return new Gson().fromJson(json, new TypeToken<List<String>>() {}.getType());
    }

    public static boolean isShopOwner(Context context) {
        return getRoles(context).contains("shop_owner");
        // 与后端返回值的身份String内容相同。
    }

    public static void clear(Context context) {
        context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE)
                .edit()
                .clear()
                .apply();
    }
}
