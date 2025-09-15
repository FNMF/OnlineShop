package com.example.merchantapp.ui;

import android.content.Intent;
import android.os.Bundle;
import android.view.Gravity;

import com.example.merchantapp.R;
import com.example.merchantapp.ui.sidemenu.ProductServiceActivity;
import com.example.merchantapp.ui.sidemenu.ProfileActivity;
import com.example.merchantapp.ui.sidemenu.SettingsActivity;
import com.example.merchantapp.ui.sidemenu.ShopSettingsActivity;
import com.google.android.material.appbar.MaterialToolbar;
import com.google.android.material.bottomnavigation.BottomNavigationView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.example.merchantapp.databinding.ActivityMainBinding;
import com.google.android.material.navigation.NavigationView;

public class MainActivity extends AppCompatActivity {

    private ActivityMainBinding binding;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        BottomNavigationView navView = findViewById(R.id.nav_view);
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        AppBarConfiguration appBarConfiguration = new AppBarConfiguration.Builder(
                R.id.navigation_home, R.id.navigation_dashboard, R.id.navigation_notifications, R.id.navigation_products)
                .build();
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment_activity_main);
        // 这个是顶部栏的默认设置，这里选择改为NoActionBar，所以去掉这一段代码
        // NavigationUI.setupActionBarWithNavController(this, navController, appBarConfiguration);
        NavigationUI.setupWithNavController(binding.navView, navController);

        // ---------------- Drawer 绑定 ----------------
        DrawerLayout drawerLayout = findViewById(R.id.drawer_layout);
        NavigationView navigationView = findViewById(R.id.nav_drawer);
        MaterialToolbar toolbar = findViewById(R.id.topAppBar);

        // 左上角汉堡按钮
        toolbar.setNavigationIcon(R.drawable.df);
        toolbar.setNavigationOnClickListener(v -> drawerLayout.openDrawer(GravityCompat.START));

        // 菜单点击事件
        navigationView.setNavigationItemSelectedListener(item -> {
            int id = item.getItemId();

            if (id == R.id.nav_profile) {
                startActivity(new Intent(this, ProfileActivity.class));
            } else if (id == R.id.nav_settings) {
                startActivity(new Intent(this, SettingsActivity.class));
            } else if (id == R.id.nav_shop_settings) {
                startActivity(new Intent(this, ShopSettingsActivity.class));
            } else if (id == R.id.nav_product_service) {
                startActivity(new Intent(this , ProductServiceActivity.class));
            } else if (id == R.id.nav_logout) {
                // TODO: 执行退出逻辑
            }

            drawerLayout.closeDrawer(GravityCompat.START);
            return true;
        });

    }

}