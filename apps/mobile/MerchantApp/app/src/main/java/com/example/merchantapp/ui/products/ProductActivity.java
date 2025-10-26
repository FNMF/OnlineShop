package com.example.merchantapp.ui.products;

import android.os.Bundle;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.Observer;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.merchantapp.R;
import com.example.merchantapp.model.product.ProductRead;
import com.example.merchantapp.ui.ProductAdapter;
import com.example.merchantapp.viewmodel.ProductViewModel;

import java.util.ArrayList;
import java.util.List;

public class ProductActivity extends AppCompatActivity {

    private ProductViewModel productViewModel;
    private RecyclerView recyclerView;
    private ProductAdapter productAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_product);

        recyclerView = findViewById(R.id.recyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        // 初始化 ViewModel
        productViewModel = new ProductViewModel(getApplication());

        // 初始化 Adapter
        productAdapter = new ProductAdapter(new ArrayList<>());
        recyclerView.setAdapter(productAdapter);

        // 观察 LiveData
        productViewModel.getProductListLiveData().observe(this, new Observer<List<ProductRead>>() {
            @Override
            public void onChanged(List<ProductRead> products) {
                if (products != null) {
                    productAdapter.setProductList(products);
                }
            }
        });

        productViewModel.getErrorLiveData().observe(this, new Observer<String>() {
            @Override
            public void onChanged(String errorMsg) {
                Toast.makeText(ProductActivity.this, errorMsg, Toast.LENGTH_SHORT).show();
            }
        });

        // 获取商品列表
        productViewModel.getAllProducts();
    }
}
