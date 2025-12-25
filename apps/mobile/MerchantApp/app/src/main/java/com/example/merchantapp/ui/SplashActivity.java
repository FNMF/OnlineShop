package com.example.merchantapp.ui;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.example.merchantapp.R;
import com.example.merchantapp.api.auth.AuthRepository;
import com.example.merchantapp.api.merchant.MerchantRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.auth.LoginResponse;
import com.example.merchantapp.storage.TokenManager;
import com.example.merchantapp.ui.auth.LoginActivity;
import com.example.merchantapp.ui.auth.PhoneActivity;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SplashActivity extends AppCompatActivity {

    private AuthRepository authRepository;
    private String refreshToken;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_splash);
        authRepository = new AuthRepository();

        autoLogin();
        }

    private void autoLogin() {
        if (!TokenManager.hasRefreshToken(this)) {
            goPhone();
            return;
        }
        refreshToken = TokenManager.getRefreshToken(this);

        authRepository.getMerchantProfile(
                new Callback<ApiResponse<AuthResponse>>() {
                    @Override
                    public void onResponse(
                            Call<ApiResponse<AuthResponse>> call,
                            Response<ApiResponse<AuthResponse>> response
                    ) {
                        if (!response.isSuccessful()
                                || response.body() == null
                                || response.body().getData() == null) {

                            TokenManager.clearAll(SplashActivity.this);
                            goPhone();
                            return;
                        }

                        AuthResponse body = response.body().getData();
                        LoginResponse login = body.getLoginResponse();

                        if (login == null) {
                            TokenManager.clearAll(SplashActivity.this);
                            goPhone();
                            return;
                        }

                        TokenManager.saveLogin(
                                SplashActivity.this,
                                login.getAccessToken(),
                                refreshToken,
                                login.getMerchant()
                        );

                        goMain();
                    }

                    @Override
                    public void onFailure(
                            Call<ApiResponse<AuthResponse>> call,
                            Throwable t
                    ) {
                        goPhone();
                    }
                });
    }

    private void goMain() {
        Intent intent = new Intent(this, MainActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        startActivity(intent);
        finish();
    }

    private void goPhone() {
        Intent intent = new Intent(this, PhoneActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        startActivity(intent);
        finish();
    }
}