package com.example.merchantapp.viewmodel;

import android.app.Application;

import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;

import com.example.merchantapp.api.product.ProductRepository;
import com.example.merchantapp.model.product.ProductRead;

import java.util.List;

public class ProductViewModel extends AndroidViewModel {
    private final ProductRepository productRepository;
    private final MutableLiveData<List<ProductRead>> productListLiveData;
    private final MutableLiveData<String> errorLiveData;

    public ProductViewModel(Application application) {
        super(application);
        productRepository = new ProductRepository();
        productListLiveData = new MutableLiveData<>();
        errorLiveData = new MutableLiveData<>();
    }

    // 获取商品列表
    public void getAllProducts() {
        productRepository.getAllProducts(new ProductRepository.ProductListCallback() {
            @Override
            public void onSuccess(List<ProductRead> products) {
                productListLiveData.postValue(products);
            }

            @Override
            public void onError(String errorMsg) {
                errorLiveData.postValue(errorMsg);
            }
        });
    }
        public LiveData<List<ProductRead>> getProductListLiveData () {
            return productListLiveData;
        }

        // 返回错误消息的 LiveData
        public LiveData<String> getErrorLiveData () {
            return errorLiveData;
        }
    }
