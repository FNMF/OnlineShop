package com.example.merchantapp.storage;

import android.content.Context;
import android.content.SharedPreferences;

import com.example.merchantapp.model.merchant.AdminMerchantResponse;
import com.google.gson.Gson;

public class ShopManager {
    private static final String SP_NAME = "shop_prefs";
    private static final String KEY_SHOP = "key_shop";
    private static Context context;
    public static void init(Context ctx) {
        context = ctx.getApplicationContext();
    }
    private static SharedPreferences sp() {
        if (context == null) {
            throw new IllegalStateException("ShopManager not initialized");
        }
        return context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
    }

    public static void saveShop(AdminMerchantResponse shop) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        String json = new Gson().toJson(shop);
        sp.edit()
                .putString(KEY_SHOP, json)
                .apply();
    }

    public static AdminMerchantResponse getShop() {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        String json = sp.getString(KEY_SHOP, null);
        if (json == null) return null;
        return new Gson().fromJson(json, AdminMerchantResponse.class);
    }

    public static boolean hasShop() {
        return getShop() != null;
    }

    public static void clear() {
        context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE)
                .edit()
                .clear()
                .apply();
    }
}
