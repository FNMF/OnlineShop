package com.example.merchantapp.ui.auth;

import androidx.lifecycle.ViewModel;

import com.example.merchantapp.api.auth.AuthRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;

import retrofit2.Callback;

public class LoginViewModel extends ViewModel {

    private final AuthRepository repository = new AuthRepository();

    public void loginByAccount(String account,
                               String password,
                               Callback<ApiResponse<AuthResponse>> callback) {
        repository.loginByAccount(account,password, callback);
    }
}