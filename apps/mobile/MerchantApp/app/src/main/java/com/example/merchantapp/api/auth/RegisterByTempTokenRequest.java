package com.example.merchantapp.api.auth;

import com.google.gson.annotations.SerializedName;

public class RegisterByTempTokenRequest {
    public String password;
    public RegisterByTempTokenRequest(String password){
        this.password = password;
    }
}
