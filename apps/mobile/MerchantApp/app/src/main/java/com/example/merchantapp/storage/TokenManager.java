package com.example.merchantapp.storage;

import android.content.Context;
import android.content.SharedPreferences;

import com.example.merchantapp.model.auth.MerchantInfo;

public class TokenManager {

    private static final String SP_NAME = "auth_prefs";

    public static void save(
            Context context,
            String accessToken,
            String refreshToken,
            MerchantInfo merchant
    ) {
        SharedPreferences sp =
                context.getSharedPreferences(SP_NAME, Context.MODE_PRIVATE);

        sp.edit()
                .putString("access_token", accessToken)
                .putString("refresh_token", refreshToken)
                .putString("merchant_uuid", merchant.getUuid())
                .putString("merchant_phone", merchant.getPhone())
                .putInt("merchant_account",merchant.getAccount())
                .apply();
    }

    public static String getAccessToken(Context context) {
        return context
                .getSharedPreferences(SP_NAME, Context.MODE_PRIVATE)
                .getString("access_token", null);
    }

    public static void clear(Context context) {
        context
                .getSharedPreferences(SP_NAME, Context.MODE_PRIVATE)
                .edit()
                .clear()
                .apply();
    }
}
