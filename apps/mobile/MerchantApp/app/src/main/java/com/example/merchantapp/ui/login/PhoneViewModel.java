package com.example.merchantapp.ui.login;

import androidx.lifecycle.ViewModel;

import com.example.merchantapp.api.auth.AuthRepository;
import com.example.merchantapp.model.auth.LoginResponse;

import retrofit2.Callback;

public class PhoneViewModel extends ViewModel {
    private final AuthRepository repository = new AuthRepository();
    public void loginByValidationCode(String phone,
                                      String code,
                                      Callback<LoginResponse> callback){
        repository.loginByValidationCode(phone,code,callback);
    }
    public void sendCode(String phone,
                             Callback<Void> callback){
        repository.sendCode(phone,callback);
    }
}
