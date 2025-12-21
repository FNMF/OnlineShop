package com.example.merchantapp.model.auth;

import com.google.gson.annotations.SerializedName;

public class AuthResponse {
    @SerializedName("isNewUser")
    public boolean isNewUser;
    @SerializedName("loginTokenResult")
    public LoginResponse loginResponse;
    @SerializedName("registerTokenResult")
    public RegisterResponse registerResponse;


    public boolean isNewUser() {
        return isNewUser;
    }

    public LoginResponse getLoginResponse() {
        return loginResponse;
    }

    public RegisterResponse getRegisterResponse() {
        return registerResponse;
    }
}
