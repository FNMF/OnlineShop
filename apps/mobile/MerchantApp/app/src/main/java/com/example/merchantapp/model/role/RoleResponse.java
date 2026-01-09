package com.example.merchantapp.model.role;

import com.google.gson.annotations.SerializedName;

import java.util.List;

public class RoleResponse {
    @SerializedName("roles")
    public List<String> roles;

    public List<String> getRoles() {
        return roles;
    }
}
