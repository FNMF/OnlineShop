package com.example.merchantapp.ui.products;

import android.os.Bundle;
import android.view.MenuItem;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.example.merchantapp.R;

public class AddProductActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_add_pruduct);

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);                 // 把 Toolbar 当作 ActionBar

        if (getSupportActionBar() != null) {
            getSupportActionBar().setDisplayHomeAsUpEnabled(true); // 显示左上角箭头
            // 可选：使用自定义图标（需先在 res/drawable/ 添加 ic_arrow_back）
            // getSupportActionBar().setHomeAsUpIndicator(R.drawable.ic_arrow_back);
        }

        // 如果想用点击监听而不是 onOptionsItemSelected，也可以：
        // toolbar.setNavigationOnClickListener(v -> onBackPressed());
    }

    // 处理 Up 按钮被点击（也会处理系统默认 home 按钮）
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == android.R.id.home) {
            // 按返回箭头的行为 — 默认我们 finish() 当前 Activity
            finish();
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

}