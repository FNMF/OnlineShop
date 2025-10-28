package com.example.merchantapp.ui.products;

import android.os.Bundle;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.example.merchantapp.R;
import com.example.merchantapp.api.product.ProductRepository;
import com.example.merchantapp.model.product.ProductReadDetail;

public class ProductDetailActivity extends AppCompatActivity {
    private TextView nameText, priceText, stockText, descText, ingredientText, weightText;
    private ImageView coverImage;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_product_detail);

        nameText = findViewById(R.id.detail_name);
        priceText = findViewById(R.id.detail_price);
        stockText = findViewById(R.id.detail_stock);
        descText = findViewById(R.id.detail_description);
        ingredientText = findViewById(R.id.detail_ingredient);
        weightText = findViewById(R.id.detail_weight);
        coverImage = findViewById(R.id.detail_cover);

        String uuid = getIntent().getStringExtra("uuid");
        if (uuid == null) {
            Toast.makeText(this, "无效商品", Toast.LENGTH_SHORT).show();
            finish();
            return;
        }

        RecyclerView imagesRecycler = findViewById(R.id.product_images_recycler);
        imagesRecycler.setLayoutManager(
                new LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
        );

        ProductRepository repository = new ProductRepository();
        repository.getProductDetail(uuid, new ProductRepository.ProductDetailCallback() {
            @Override
            public void onSuccess(ProductReadDetail product) {
                runOnUiThread(() -> {
                    nameText.setText(product.getName());
                    priceText.setText("￥" + product.getPrice());
                    stockText.setText("库存：" + product.getStock());
                    descText.setText(product.getDescription());
                    ingredientText.setText(product.getIngredient());
                    weightText.setText(product.getWeight());
                    Glide.with(ProductDetailActivity.this)
                            .load(product.getCoverUrl())               // URL（可能为 null）
                            .placeholder(R.drawable.ic_placeholder)    // 占位图（在 res/drawable）
                            .error(R.drawable.ic_image_error)          // 加载失败时图
                            .into(coverImage);
                    // 多图
                    if (product.getImages() != null && !product.getImages().isEmpty()) {
                        ProductImageAdapter imageAdapter = new ProductImageAdapter(product.getImages());
                        imagesRecycler.setAdapter(imageAdapter);
                    }
                });
            }

            @Override
            public void onError(String errorMsg) {
                runOnUiThread(() ->
                        Toast.makeText(ProductDetailActivity.this, errorMsg, Toast.LENGTH_SHORT).show()
                );
            }
        });
    }
}
