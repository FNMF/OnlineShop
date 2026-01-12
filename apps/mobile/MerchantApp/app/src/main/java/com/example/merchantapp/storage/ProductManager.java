package com.example.merchantapp.storage;


import android.content.Context;
import android.content.SharedPreferences;

public class ProductManager {
    private static SharedPreferences prefs;
    private static ProductManager instance;

    private static final String PREF_NAME = "product_prefs";

    public static void init(Context context) {
        prefs = context.getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE);
    }
    public static ProductManager getInstance() {
        if (instance == null) {
            instance = new ProductManager();
        }
        return instance;
    }
}
