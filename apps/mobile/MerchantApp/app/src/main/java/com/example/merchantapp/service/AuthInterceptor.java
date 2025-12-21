package com.example.merchantapp.service;

import android.content.Context;

import com.example.merchantapp.storage.TokenManager;

import java.io.IOException;

import okhttp3.Interceptor;
import okhttp3.Request;
import okhttp3.Response;

public class AuthInterceptor implements Interceptor {

    private final Context context;

    public AuthInterceptor(Context context) {
        this.context = context.getApplicationContext();
    }

    @Override
    public Response intercept(Chain chain) throws IOException {

        String access = TokenManager.getAccessToken(context);
        String temp = TokenManager.getTempToken(context);

        Request original = chain.request();
        Request.Builder builder = original.newBuilder();

        if (access != null&& !access.isEmpty()) {
            builder.addHeader("Authorization", "Bearer " + access);
        } else if (temp != null&& !temp.isEmpty()) {
            builder.addHeader("Authorization", "Bearer " + temp);
        }

        return chain.proceed(builder.build());
    }
}

