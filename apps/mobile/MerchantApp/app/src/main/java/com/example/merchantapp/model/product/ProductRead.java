package com.example.merchantapp.model.product;

import java.math.BigDecimal;

public class ProductRead {
    private String ProductUuid;
    private String Name;
    private BigDecimal Price;
    private int Stock;
    private String Weight;
    private boolean IsListed;
    private boolean IsAvailable;
    private String CoverUrl;
    private BigDecimal PackingFee;
    public String getProductUuid() {
        return ProductUuid;
    }

    public void setProductUuid(String productUuid) {
        ProductUuid = productUuid;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public BigDecimal getPrice() {
        return Price;
    }

    public void setPrice(BigDecimal price) {
        Price = price;
    }

    public int getStock() {
        return Stock;
    }

    public void setStock(int stock) {
        Stock = stock;
    }

    public String getWeight() {
        return Weight;
    }

    public void setWeight(String weight) {
        Weight = weight;
    }

    public boolean isListed() {
        return IsListed;
    }

    public void setListed(boolean listed) {
        IsListed = listed;
    }

    public boolean isAvailable() {
        return IsAvailable;
    }

    public void setAvailable(boolean available) {
        IsAvailable = available;
    }

    public String getCoverUrl() {
        return CoverUrl;
    }

    public void setCoverUrl(String coverUrl) {
        CoverUrl = coverUrl;
    }

    public BigDecimal getPackingFee() {
        return PackingFee;
    }

    public void setPackingFee(BigDecimal packingFee) {
        PackingFee = packingFee;
    }
}
