package com.example.merchantapp.model.auth;

public class AuthResponse {
    public boolean isNewUser;
    public LoginResponse loginResponse;
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
