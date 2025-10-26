package com.example.merchantapp.ui;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.merchantapp.R;
import com.example.merchantapp.model.product.ProductRead;

import java.util.ArrayList;
import java.util.List;

public class ProductAdapter extends RecyclerView.Adapter<ProductAdapter.ProductViewHolder> {

    private List<ProductRead> productList = new ArrayList<>();
    // 添加构造函数，接收一个 List<ProductRead> 参数
    public ProductAdapter(List<ProductRead> productList) {
        this.productList = productList;
    }

    @Override
    public ProductViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.fragment_item, parent, false);
        return new ProductViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ProductViewHolder holder, int position) {
        ProductRead product = productList.get(position);
        holder.productName.setText(product.getName());
        holder.productPrice.setText("￥" + product.getPrice());
        holder.productStock.setText("库存：" + product.getStock());
    }

    @Override
    public int getItemCount() {
        return productList.size();
    }

    public void setProductList(List<ProductRead> products) {
        productList.clear();
        productList.addAll(products);
        notifyDataSetChanged();
    }

    public static class ProductViewHolder extends RecyclerView.ViewHolder {
        TextView productName, productPrice, productStock;

        public ProductViewHolder(View itemView) {
            super(itemView);
            productName = itemView.findViewById(R.id.product_name);
            productPrice = itemView.findViewById(R.id.product_price);
            productStock = itemView.findViewById(R.id.product_stock);
        }
    }
}
