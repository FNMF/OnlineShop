package com.example.merchantapp.storage;

import android.content.Context;
import android.content.SharedPreferences;

import com.example.merchantapp.model.merchant.AdminMerchantResponse;
import com.google.gson.Gson;

public class ShopManager {
    private static final String SP_NAME = "shop_prefs";
    private static final String KEY_SHOP = "key_shop";

    public static void saveShop(Context context, AdminMerchantResponse shop) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        String json = new Gson().toJson(shop);
        sp.edit()
                .putString(KEY_SHOP, json)
                .apply();
    }

    public static AdminMerchantResponse getShop(Context context) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        String json = sp.getString(KEY_SHOP, null);
        if (json == null) return null;
        return new Gson().fromJson(json, AdminMerchantResponse.class);
    }

    public static boolean hasShop(Context context) {
        return getShop(context) != null;
    }

    public static void clear(Context context) {
        context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE)
                .edit()
                .clear()
                .apply();
    }
}
