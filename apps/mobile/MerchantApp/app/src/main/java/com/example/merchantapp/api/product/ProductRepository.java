package com.example.merchantapp.api.product;

import android.util.Log;

import com.example.merchantapp.api.ApiClient;
import com.example.merchantapp.model.product.ProductRead;

import java.io.File;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ProductRepository {
    private final ProductApiService apiService;

    public ProductRepository() {
        this.apiService = ApiClient.getClient().create(ProductApiService.class);
    }

    /**
     * 上传商品（添加商品）
     */
    public void addProduct(
            String name,
            String price,
            String packingFee,
            String stock,
            String desc,
            String ingredient,
            String weight,
            boolean isListed,
            boolean isAvailable,
            File coverFile,
            List<File> imageFiles,
            ProductCallback callback
    ) {
        // 调用 MultipartHelper 构造请求
        Call<ProductRead> call = MultipartHelper.createProductRequest(
                apiService,
                name, price, packingFee, stock, desc, ingredient, weight,
                isListed, isAvailable,
                coverFile, imageFiles
        );

        // 异步发送请求
        call.enqueue(new Callback<ProductRead>() {
            @Override
            public void onResponse(Call<ProductRead> call, Response<ProductRead> response) {
                if (response.isSuccessful() && response.body() != null) {
                    callback.onSuccess(response.body());
                } else {
                    callback.onError("添加商品失败：" + response.message());
                }
            }

            @Override
            public void onFailure(Call<ProductRead> call, Throwable t) {
                Log.e("ProductRepository", "网络请求失败", t);
                callback.onError("网络错误：" + t.getMessage());
            }
        });
    }

    // 其他接口，比如获取全部商品
    public void getAllProducts(ProductListCallback callback) {
        apiService.getProductReads().enqueue(new Callback<List<ProductRead>>() {
            @Override
            public void onResponse(Call<List<ProductRead>> call, Response<List<ProductRead>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    callback.onSuccess(response.body());
                } else {
                    callback.onError("获取商品失败：" + response.message());
                }
            }

            @Override
            public void onFailure(Call<List<ProductRead>> call, Throwable t) {
                callback.onError("网络错误：" + t.getMessage());
            }
        });
    }

    // 获取单个商品
    public void getProductByUuid(String uuid, ProductCallback callback) {
        apiService.getProductByUuid(uuid).enqueue(new Callback<ProductRead>() {
            @Override
            public void onResponse(Call<ProductRead> call, Response<ProductRead> response) {
                if (response.isSuccessful() && response.body() != null) {
                    callback.onSuccess(response.body());
                } else {
                    callback.onError("获取商品详情失败：" + response.message());
                }
            }

            @Override
            public void onFailure(Call<ProductRead> call, Throwable t) {
                callback.onError("网络错误：" + t.getMessage());
            }
        });
    }

    // 更新
    public void updateProduct(
            String uuid,
            String name,
            String price,
            String packingFee,
            String stock,
            String desc,
            String ingredient,
            String weight,
            boolean isListed,
            boolean isAvailable,
            File coverFile,
            List<File> imageFiles,
            ProductCallback callback
    ) {
        Call<ProductRead> call = MultipartHelper.createUpdateProductRequest(
                apiService,
                uuid,
                name, price, packingFee, stock, desc, ingredient, weight,
                isListed, isAvailable,
                coverFile, imageFiles
        );

        call.enqueue(new Callback<ProductRead>() {
            @Override
            public void onResponse(Call<ProductRead> call, Response<ProductRead> response) {
                if (response.isSuccessful() && response.body() != null) {
                    callback.onSuccess(response.body());
                } else {
                    callback.onError("更新商品失败：" + response.message());
                }
            }

            @Override
            public void onFailure(Call<ProductRead> call, Throwable t) {
                callback.onError("网络错误：" + t.getMessage());
            }
        });
    }

    // 删除
    public void deleteProduct(String uuid, SimpleCallback callback) {
        apiService.deleteProduct(uuid).enqueue(new Callback<Void>() {
            @Override
            public void onResponse(Call<Void> call, Response<Void> response) {
                if (response.isSuccessful()) {
                    callback.onSuccess();
                } else {
                    callback.onError("删除失败：" + response.message());
                }
            }

            @Override
            public void onFailure(Call<Void> call, Throwable t) {
                callback.onError("网络错误：" + t.getMessage());
            }
        });
    }

    // 回调接口定义
    public interface ProductCallback {
        void onSuccess(ProductRead product);
        void onError(String errorMsg);
    }

    public interface ProductListCallback {
        void onSuccess(List<ProductRead> products);
        void onError(String errorMsg);
    }
    public interface SimpleCallback {
        void onSuccess();
        void onError(String errorMsg);
    }
}
