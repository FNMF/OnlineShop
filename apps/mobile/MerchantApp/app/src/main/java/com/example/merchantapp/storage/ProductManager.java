package com.example.merchantapp.storage;


import android.content.Context;
import android.content.SharedPreferences;

import com.example.merchantapp.model.product.ProductRead;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

public class ProductManager {

    private static final String SP_NAME = "product_prefs";
    private static final String KEY_PRODUCTS = "key_products";
    public static void saveProducts(Context context, List<ProductRead> products) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        String json = new Gson().toJson(products);
        sp.edit()
                .putString(KEY_PRODUCTS, json)
                .apply();
    }
    public static List<ProductRead> getProducts(Context context) {
        SharedPreferences sp = context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);
        String json = sp.getString(KEY_PRODUCTS, null);
        if (json == null) return new ArrayList<>();

        Type type = new TypeToken<List<ProductRead>>() {}.getType();
        return new Gson().fromJson(json, type);
    }
    public static void clear(Context context) {
        context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE)
                .edit()
                .clear()
                .apply();
    }
}
