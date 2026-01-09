package com.example.merchantapp.api.auth;

public class RegisterByTempTokenRequest {
    public String password;
    public RegisterByTempTokenRequest(String password){
        this.password = password;
    }
}
