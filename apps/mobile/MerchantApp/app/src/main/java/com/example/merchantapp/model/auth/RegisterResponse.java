package com.example.merchantapp.model.auth;

public class RegisterResponse {
    private String tempToken;
    private int expiresIn;

    public String getTempToken() {
        return tempToken;
    }

    public int getExpiresIn() {
        return expiresIn;
    }
}
