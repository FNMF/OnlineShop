package com.example.merchantapp.ui.auth;

import android.content.Context;

import androidx.lifecycle.ViewModel;

import com.example.merchantapp.api.auth.AuthRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;

import retrofit2.Callback;

public class RegisterPasswordViewModel extends ViewModel {
    private final AuthRepository repository;
    public RegisterPasswordViewModel() {
        repository = new AuthRepository();
    }
    public void registerPassword(String password,
                                 Callback<ApiResponse<AuthResponse>> callback){
        repository.registerByTempToken(password,callback);
    }
}
