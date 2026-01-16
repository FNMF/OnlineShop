package com.example.merchantapp.ui;

import android.content.Intent;
import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import com.example.merchantapp.R;
import com.example.merchantapp.api.UserSessionRepository;
import com.example.merchantapp.api.role.RoleRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.storage.RoleManager;
import com.example.merchantapp.storage.SessionManager;
import com.example.merchantapp.ui.auth.PhoneActivity;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PostLoginLoadingActivity extends AppCompatActivity {

    private final UserSessionRepository userSessionRepository = new UserSessionRepository();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_post_login_loading);

        initAfterLogin();
    }

    private void initAfterLogin() {

        new UserSessionRepository().fetchSession(
                this,
                new UserSessionRepository.Callback() {
                    @Override
                    public void onSuccess() {
                        goMain();
                    }

                    @Override
                    public void onError() {
                        // 理论上极少发生，除非登录异常
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