<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="E_commerce.WebForm10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container tm-mt-big tm-mb-big mt-5 mb-5">
        <div class="row">
            <div class="col-xl-9 col-lg-10 col-md-12 col-sm-12 mx-auto">
                <div class="tm-bg-primary-dark tm-block tm-block-h-auto">
                    <div class="row">
                        <div class="col-12">
                            <h2 class="tm-block-title d-inline-block">Add Product</h2>
                            <br />
                            <h4 class="tm-block-title d-inline-block">Please fill out the product information before you upload any images</h4>
                        </div>
                    </div>

                    <div class="row tm-edit-product-row">
                        <div class="col-xl-6 col-lg-6 col-md-12">
                            <div class="form-group mb-3">
                                <label >Product Name(*)</label>
                                <input id="Name" name="name" type="text" class="form-control validate" required="">
                            </div>

                            <div class="form-group mb-3">
                                <label >Description(*)</label>
                                <textarea id="Description" class="form-control validate" rows="3" required=""></textarea>
                            </div>

                            <div class="form-group mb-3">
                                <label >Category(*)</label>
                                <select class="custom-select tm-select-accounts" id="Category">
                                    <option selected="">Select category</option>
                                    <option value="1">Smartphone</option>
                                    <option value="2">Accessories</option>
                                    <option value="3">Smartwatch</option>
                                    <option value="4">Gaming</option>
                                </select>
                            </div>

                            <div class="form-group mb-3">
                                <label >Brand(*)</label>
                                <select class="custom-select tm-select-accounts" id="Brand">
                                    <option selected="">Select category</option>
                                    <option value="1">SAMSUNG</option>
                                    <option value="2">Apple</option>
                                    <option value="3">Xiaomi</option>
                                </select>
                            </div>

                            <div class="form-group mb-3">
                                <div class="form-group mb-3 col-xs-12 col-sm-6">
                                    <label >Price(*) :</label>
                                    <input id="Price" name="stock" type="text" class="form-control validate" required="">
                                </div>
                            </div>

                            <div class="form-group mb-3">
                                <div class="form-group mb-3 col-xs-12 col-sm-6">
                                    <label >Memory :</label>
                                    <select class="custom-select tm-select-accounts" id="Memory">
                                        <option selected="">Select category</option>
                                        <option value="1">16 GB</option>
                                        <option value="2">32 GB</option>
                                        <option value="3">64 GB</option>
                                        <option value="4">128 GB</option>
                                        <option value="5">256 GB</option>
                                        <option value="6">512 GB</option>
                                    </select>
                                </div>
                            </div>

                            <div class="form-group mb-3">
                                <div class="form-group mb-3 col-xs-12 col-sm-6">
                                    <label >Screen(inch) :</label>
                                    <input id="Screen" name="stock" type="number" class="form-control validate" required="">
                                </div>
                            </div>

                            <div class="form-group mb-3">
                                <div class="form-group mb-3 col-xs-12 col-sm-6">
                                    <label >Battery(mAH) :</label>
                                    <input id="Battery" name="stock" type="number" class="form-control validate" required="">
                                </div>
                            </div>

                            <div class="form-group mb-3">
                                <div class="form-group mb-3 col-xs-12 col-sm-6">
                                    <label >Processor :</label>
                                    <input id="Processor" name="stock" type="number" class="form-control validate" required="">
                                </div>
                            </div>

                            <div class="form-group mb-3">
                                <div class="form-group mb-3 col-xs-12 col-sm-6">
                                    <label >Camera(MP) :</label>
                                    <input id="Camera" name="stock" type="number" class="form-control validate" required="">
                                </div>
                            </div>
                        </div>

                        <div class="col-xl-6 col-lg-6 col-md-12 mx-auto mb-4">
                            <div class="tm-product-img-dummy mx-auto">
                                <img id="imgShowCase" src="/img/DefaultProduct.jpg" style="width: 100%; object-fit: contain; border: 2px solid black"/>
                            </div>

                            <div class="custom-file m-5">
                                <input id="fileInput" type="file">
                                <hr />
                            </div>

                            <div class="row">
                                <div class="tm-product-img-dummy col-md-4">
                                    <img id="imgShowCaseMini0" src="/img/DefaultProduct.jpg" style="width: 100%; object-fit: contain; border: 2px solid black"/>
                                    <img class="deleteImg" src="/img/close.png" style="width: 15%; border-radius: 100px; border: 2px solid red; position: absolute; top: 5px; right: 18px; cursor: pointer;"/>
                                </div>

                                <div class="tm-product-img-dummy col-md-4">
                                    <img id="imgShowCaseMini1" src="/img/DefaultProduct.jpg" style="width: 100%; object-fit: contain; border: 2px solid black"/>
                                    <img class="deleteImg" src="/img/close.png" style="width: 15%; border-radius: 100px; border: 2px solid red; position: absolute; top: 5px; right: 18px; cursor: pointer;"/>
                                </div>

                                <div class="tm-product-img-dummy col-md-4">
                                    <img id="imgShowCaseMini2" src="/img/DefaultProduct.jpg" style="width: 100%; object-fit: contain; border: 2px solid black; position: relative;"/>
                                    <img class="deleteImg" src="/img/close.png" style="width: 15%; border-radius: 100px; border: 2px solid red; position: absolute; top: 5px; right: 18px; cursor: pointer;"/>
                                </div>

                                <div class="custom-file m-5">
                                    <input id="fileInputMini" type="file">
                                    <hr />
                                </div>
                            </div>
                        </div>

                        <div class="col-12 text-center">
                            <input type="button" id="saveProductInfo" value="Add product now" class="btn btn-primary btn-block text-uppercase"></input>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="/js/AddProduct.js"></script>
</asp:Content>
