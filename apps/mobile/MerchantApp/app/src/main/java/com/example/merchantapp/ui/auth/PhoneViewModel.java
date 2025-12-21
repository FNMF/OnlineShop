package com.example.merchantapp.ui.auth;

import androidx.lifecycle.ViewModel;

import com.example.merchantapp.api.auth.AuthRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;

import retrofit2.Callback;

public class PhoneViewModel extends ViewModel {
    private final AuthRepository repository = new AuthRepository();
    public void loginByValidationCode(String phone,
                                      String code,
                                      Callback<ApiResponse<AuthResponse>> callback){
        repository.loginByValidationCode(phone,code,callback);
    }
    public void sendCode(String phone,
                             Callback<ApiResponse<Object>> callback){
        repository.sendCode(phone,callback);
    }
}
