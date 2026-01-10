package com.example.merchantapp.ui;

import android.content.Intent;
import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import com.example.merchantapp.R;
import com.example.merchantapp.api.role.RoleRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.storage.RoleManager;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PostLoginLoadingActivity extends AppCompatActivity {

    private final RoleRepository roleRepository = new RoleRepository();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_post_login_loading);

        initAfterLogin();
    }

    private void initAfterLogin() {

        //  获取角色
        roleRepository.getUserRoles(new Callback<ApiResponse<List<String>>>() {
            @Override
            public void onResponse(Call<ApiResponse<List<String>>> call,
                                   Response<ApiResponse<List<String>>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    List<String> roles = response.body().getData();
                        RoleManager.saveRoles(
                                PostLoginLoadingActivity.this,
                                roles);
                    }
                goMain();
            }

            @Override
            public void onFailure(Call<ApiResponse<List<String>>> call, Throwable t) {
                goMain();
            }
        });

        // TODO，后续可能的获取各种内容
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
}