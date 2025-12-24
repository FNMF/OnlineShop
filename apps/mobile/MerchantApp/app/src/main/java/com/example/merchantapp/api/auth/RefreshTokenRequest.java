package com.example.merchantapp.api.auth;

public class RefreshTokenRequest {
    public String refreshToken;
    public RefreshTokenRequest(String refreshToken){
        this.refreshToken = refreshToken;
    }
}
