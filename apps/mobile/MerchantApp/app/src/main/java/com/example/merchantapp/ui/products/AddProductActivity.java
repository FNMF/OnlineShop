package com.example.merchantapp.ui.products;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.view.MenuItem;
import android.view.View;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.example.merchantapp.R;
import com.example.merchantapp.api.product.ProductRepository;
import com.example.merchantapp.model.product.ProductRead;
import com.example.merchantapp.utils.FileUtils;
import com.google.android.material.button.MaterialButton;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

public class AddProductActivity extends AppCompatActivity {

    private static final int REQUEST_COVER = 101;
    private static final int REQUEST_IMAGES = 102;

    private EditText editName, editPrice, editStock, editDesc, editWeight;
    private Button btnPickCover, btnPickImages, btnSubmit;

    private File coverFile;
    private final List<File> imageFiles = new ArrayList<>();

    private final ProductRepository repository = new ProductRepository();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_product_add);

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        if (getSupportActionBar() != null) {
            getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        }

        initViews();
        setupListeners();
    }

    private void initViews() {
        editName = findViewById(R.id.edit_name);
        editPrice = findViewById(R.id.edit_price);
        editStock = findViewById(R.id.edit_stock);
        editDesc = findViewById(R.id.edit_desc);
        editWeight = findViewById(R.id.edit_weight);

        btnPickCover = findViewById(R.id.btn_pick_cover);
        btnPickImages = findViewById(R.id.btn_pick_images);
        btnSubmit = findViewById(R.id.btn_submit);
    }

    private void setupListeners() {
        btnPickCover.setOnClickListener(v -> pickImage(REQUEST_COVER));
        btnPickImages.setOnClickListener(v -> pickImage(REQUEST_IMAGES));

        btnSubmit.setOnClickListener(v -> submitProduct());
    }

    private void pickImage(int requestCode) {
        Intent intent = new Intent(Intent.ACTION_PICK, MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
        intent.setType("image/*");
        if (requestCode == REQUEST_IMAGES) {
            intent.putExtra(Intent.EXTRA_ALLOW_MULTIPLE, true);
        }
        startActivityForResult(intent, requestCode);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (resultCode != Activity.RESULT_OK || data == null) return;

        if (requestCode == REQUEST_COVER) {
            Uri uri = data.getData();
            if (uri != null) {
                coverFile = FileUtils.uriToFile(this, uri);
                Toast.makeText(this, "已选择封面", Toast.LENGTH_SHORT).show();
            }
        } else if (requestCode == REQUEST_IMAGES) {
            imageFiles.clear();
            if (data.getClipData() != null) {
                int count = data.getClipData().getItemCount();
                for (int i = 0; i < count; i++) {
                    Uri uri = data.getClipData().getItemAt(i).getUri();
                    imageFiles.add(FileUtils.uriToFile(this, uri));
                }
            } else if (data.getData() != null) {
                imageFiles.add(FileUtils.uriToFile(this, data.getData()));
            }
            Toast.makeText(this, "已选择 " + imageFiles.size() + " 张附图", Toast.LENGTH_SHORT).show();
        }
    }

    private void submitProduct() {
        String name = editName.getText().toString().trim();
        String price = editPrice.getText().toString().trim();
        String stock = editStock.getText().toString().trim();
        String desc = editDesc.getText().toString().trim();
        String weight = editWeight.getText().toString().trim();

        if (name.isEmpty() || price.isEmpty() || stock.isEmpty() || coverFile == null) {
            Toast.makeText(this, "请填写必要信息并选择封面图", Toast.LENGTH_SHORT).show();
            return;
        }

        // 启动加载状态
        setLoadingState(true);

        repository.addProduct(
                name,
                price,
                "0", // 包装费暂时默认0
                stock,
                desc,
                "",   // 配料暂时空
                weight,
                true,  // 默认上架
                true,  // 默认可用
                coverFile,
                imageFiles,
                new ProductRepository.ProductCallback() {
                    @Override
                    public void onSuccess(ProductRead product) {
                        runOnUiThread(() -> {
                            Toast.makeText(AddProductActivity.this, "添加成功！", Toast.LENGTH_SHORT).show();
                            finish();
                        });
                    }

                    @Override
                    public void onError(String errorMsg) {
                        runOnUiThread(() ->
                                Toast.makeText(AddProductActivity.this, errorMsg, Toast.LENGTH_SHORT).show());
                    }
                }
        );
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == android.R.id.home) {
            finish();
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    private void setLoadingState(boolean isLoading) {
        ImageView loadingIcon = findViewById(R.id.loading_icon);
        MaterialButton btnSubmit = findViewById(R.id.btn_submit);

        if (isLoading) {
            btnSubmit.setEnabled(false);
            btnSubmit.setText("正在上传...");
            loadingIcon.setVisibility(View.VISIBLE);

            Animation rotateAnim = AnimationUtils.loadAnimation(this, R.anim.rotate_loading);
            loadingIcon.startAnimation(rotateAnim);

        } else {
            btnSubmit.setEnabled(true);
            btnSubmit.setText("提交商品");
            btnSubmit.setIcon(null);
        }
    }

}