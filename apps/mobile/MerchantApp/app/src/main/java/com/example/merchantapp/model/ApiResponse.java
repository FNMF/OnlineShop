package com.example.merchantapp.model;

public class ApiResponse<T> {
    private T data;
    private int code;
    private String message;
    private boolean isSuccess;

    public T getData() { return data; }
    public int getCode() { return code; }
    public String getMessage() { return message; }
    public boolean isSuccess() { return isSuccess; }
}

