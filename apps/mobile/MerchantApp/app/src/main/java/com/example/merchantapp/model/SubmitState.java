package com.example.merchantapp.model;

public class SubmitState {
    public enum Status {
        IDLE,
        LOADING,
        SUCCESS,
        ERROR
    }

    public Status status = Status.IDLE;
    public String errorMessage;

    public static SubmitState loading() {
        SubmitState s = new SubmitState();
        s.status = Status.LOADING;
        return s;
    }

    public static SubmitState success() {
        SubmitState s = new SubmitState();
        s.status = Status.SUCCESS;
        return s;
    }

    public static SubmitState error(String msg) {
        SubmitState s = new SubmitState();
        s.status = Status.ERROR;
        s.errorMessage = msg;
        return s;
    }
}
