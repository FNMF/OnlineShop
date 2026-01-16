package com.example.merchantapp.ui;

import android.content.Intent;
import android.os.Bundle;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;

import com.example.merchantapp.R;
import com.example.merchantapp.api.UserSessionRepository;
import com.example.merchantapp.api.auth.AuthRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.auth.LoginResponse;
import com.example.merchantapp.storage.SessionManager;
import com.example.merchantapp.storage.TokenManager;
import com.example.merchantapp.ui.auth.PhoneActivity;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SplashActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_splash);

        autoLogin();
    }

    private void autoLogin() {
        if (!SessionManager.get().isLoggedIn()) {
            goPhone();
            return;
        }

        // 冷启动：有 token，初始化 Session
        new UserSessionRepository().fetchSession(
                this,
                new UserSessionRepository.Callback() {
                    @Override
                    public void onSuccess() {
                        goMain();
                    }

                    @Override
                    public void onError() {
                        // Session 构建失败，通常是 token 已失效
                        SessionManager.get().logout();
                        goPhone();
                    }
                }
        );
    }

    private void goMain() {
        Intent intent = new Intent(this, MainActivity.class);
        intent.setFlags(
                Intent.FLAG_ACTIVITY_NEW_TASK |
                        Intent.FLAG_ACTIVITY_CLEAR_TASK
        );
        startActivity(intent);
        finish();
    }

    private void goPhone() {
        Intent intent = new Intent(this, PhoneActivity.class);
        intent.setFlags(
                Intent.FLAG_ACTIVITY_NEW_TASK |
                        Intent.FLAG_ACTIVITY_CLEAR_TASK
        );
        startActivity(intent);
        finish();
    }
}
