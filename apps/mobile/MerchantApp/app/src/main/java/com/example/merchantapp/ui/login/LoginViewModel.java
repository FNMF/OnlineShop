package com.example.merchantapp.ui.login;

import androidx.lifecycle.ViewModel;

import com.example.merchantapp.api.auth.AuthRepository;
import com.example.merchantapp.api.auth.LoginByAccountRequest;
import com.example.merchantapp.api.auth.LoginByTokenRequest;
import com.example.merchantapp.model.auth.LoginResponse;

import retrofit2.Callback;

public class LoginViewModel extends ViewModel {

    private final AuthRepository repository = new AuthRepository();

    public void loginByAccount(String account,
                               String password,
                               Callback<LoginResponse> callback) {
        repository.loginByAccount(account,password, callback);
    }

    public void loginByPhone(String refreshToken,
                             Callback<LoginResponse> callback) {
        repository.loginByToken(refreshToken, callback);
    }
}