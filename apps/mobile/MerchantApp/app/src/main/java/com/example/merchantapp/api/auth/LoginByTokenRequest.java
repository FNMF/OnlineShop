package com.example.merchantapp.api.auth;

public class LoginByTokenRequest {
    public String refreshToken;
    public LoginByTokenRequest(String refreshToken){
        this.refreshToken = refreshToken;
    }
}
