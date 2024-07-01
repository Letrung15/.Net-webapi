import React, { useEffect, useState } from "react";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import { Navigate } from "react-router-dom";
import MenuItem from "@mui/material/MenuItem";
import { CButton, CCol, CContainer, CFormInput, CFormLabel, CFormTextarea, CRow } from "@coreui/react";
import { GET_ALL_CATEGORIES, GET_ALL_TAGS, POST_ADD_PRODUCT } from "../api/apiService";
import { Image } from "react-bootstrap";
import axios from "axios";
import { Select } from "@mui/material";

export default function Product() {
  const [checkAdd, setCheckAdd] = useState(false);
  const [productName, setProductName] = useState("");
  const [regularPrice, setRegularPrice] = useState(null);
  const [discountPrice, setDiscountPrice] = useState(null);
  const [productDescription, setProductDescription] = useState('');
  const [quantity, setQuantity] = useState(null);
  const [shortDescription, setShortDescription] = useState('');
  // const [published, setPublished] = useState(true);
  const [note, setNote] = useState('');
  const [categories, setCategories] = useState([]);
  const [categoryAll, setCategoryAll] = useState([]);
  const [tags, setTags] = useState([]);
  const [tagAll, setTagAll] = useState([]);
  const [selectedImages, setSelectedImages] = useState([]);
  const [imageFiles, setImageFiles] = useState([]);

  useEffect(() => {
    GET_ALL_CATEGORIES("Categories").then((item) => {
      setCategoryAll(item.data);
    });
    GET_ALL_TAGS("Tag").then((item) => {
      setTagAll(item.data);
    });
  }, []);

  const handleChangeTitle = (e) => {
    setProductName(e.target.value);
  };

  const handleChangeBody = (e) => {
    setProductDescription(e.target.value);
  };

  const handleChangePriceRegular = (e) => {
    setRegularPrice(e.target.value);
  };

  const handleChangeQuantity = (e) => {
    setQuantity(e.target.value);
  };

  const handleChangeShortDescription = (e) => {
    setShortDescription(e.target.value);
  };

  // const handleChangePublished = (e) => {
  //   setPublished(e.target.value);
  // };

  const handleChangeNote = (e) => {
    setNote(e.target.value);
  };

  const handleChangePriceDiscount = (e) => {
    setDiscountPrice(e.target.value);
  };

  const handleChangeCategories = (event) => {
    const selectedIds = event.target.value;
    setCategories(selectedIds);
  };

  const handleChangeTags = (event) => {
    const selectedIds = event.target.value;
    setTags(selectedIds);
  };

  const handleFileChange = (event) => {
    const files = event.target.files;
    const imagesArray = [];
    const filesArray = [];
    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      const reader = new FileReader();
      reader.onloadend = () => {
        imagesArray.push(reader.result);
        if (imagesArray.length === files.length) {
          setSelectedImages([...selectedImages, ...imagesArray]);
          setImageFiles([...imageFiles, ...filesArray]);
        }
      };
      if (file) {
        reader.readAsDataURL(file);
        filesArray.push(file);
      }
    }
  };

  const handleResetImages = () => {
    setSelectedImages([]);
    setImageFiles([]);
  };

  const addProduct = async (e) => {
    e.preventDefault();
  
    if (productName !== '' && productDescription !== '' && regularPrice !== null && discountPrice !== null && categories.length > 0) {
      const salePriceNumber = parseFloat(regularPrice);
      const buyingPriceNumber = parseFloat(discountPrice);
  
      let product = {
        productName: productName,
        salePrice: salePriceNumber,
        buyingPrice: buyingPriceNumber,
        quantity: quantity,
        shortDescription: shortDescription,
        productDescription: productDescription,
        // published: published,
        note: note,
        productCategoryIds: categories,        
        productTagIds: tags,        
        productCategoryNames: categories.map((categoryId) => categoryAll.find((category) => category.id === categoryId).categoryName),
        productTagsNames: tags.map((tagId) => tagAll.find((tag) => tag.id === tagId).tagName),
      };
  console.log(product)
      try {
        const response = await POST_ADD_PRODUCT(`Product`, product);
        if (response.status === 201 || response.status === 200) {
          console.log("idr0", response)
          handleUploadImages(response.data.id);
        }
      } catch (error) {
        console.error("Error:", error);
        alert("Error occurred while adding product!");
      }
    }
  };
  

  const handleUploadImages = async (id) => {
    const formData = new FormData();
    formData.append('ProductId', id); // Thêm dòng này để gửi id của sản phẩm
    imageFiles.forEach((image) => {
      formData.append("imageFiles", image);
    });
    formData.forEach((value, key) => {
        console.log(`${key}: ${value}`);
      });
    try {
      const response = await axios.post(
        `http://localhost:5284/api/Gallery/multiple/`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );

      if (response.status === 200) {
        setCheckAdd(true);
      } else {
        alert("Error occurred while uploading images!33333");
      }
    } catch (error) {
      console.error("Error:", error);
      alert("Error occurred while uploading images!");
    }
  };

  if (checkAdd) {
    return <Navigate to="/" />;
  }

  return (
    <div>
      <Grid container spacing={3} style={{ marginBottom: 40 }}>
  <Grid item xs={12}>
    <CContainer
      className="p-3 border rounded"
      style={{ width: 800, margin: "0 auto" }}
    >
      <h3 variant="h4" style={{ textAlign: "center" }}>
        Add Product
      </h3>
      <Grid container spacing={3}>
        <Grid item xs={4}>
          {/* Cột 1 */}
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Title
              </CFormLabel>
              <CFormInput
                id="title"
                onChange={handleChangeTitle}
                variant="outlined"
                size="small"
              />
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Product Description
              </CFormLabel>
              <CFormTextarea
                id="productDescription"
                onChange={handleChangeBody}
                multiline
                rows={4}
                variant="outlined"
              />
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Regular Price
              </CFormLabel>
              <CFormInput
                id="regularPrice"
                type="number"
                onChange={handleChangePriceRegular}
                variant="outlined"
                size="small"
              />
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Discount Price
              </CFormLabel>
              <CFormInput
                id="discountPrice"
                type="number"
                onChange={handleChangePriceDiscount}
                variant="outlined"
                size="small"
              />
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={4}>
          {/* Cột 2 */}
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Quantity
              </CFormLabel>
              <CFormInput
                id="quantity"
                type="number"
                onChange={handleChangeQuantity}
                variant="quantity"
                size="small"
              />
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Short Description
              </CFormLabel>
              <CFormInput
                id="shortDescription"
                onChange={handleChangeShortDescription}
                variant="outlined"
                size="small"
              />
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Note
              </CFormLabel>
              <CFormInput
                id="note"
                onChange={handleChangeNote}
                variant="outlined"
                size="small"
              />
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={4}>
          {/* Cột 3 */}
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Tag
              </CFormLabel>
              <TextField
                id="tags"
                name="tags"
                select
                style={{ width: "100%" }}
                value={tags}
                onChange={handleChangeTags}
                SelectProps={{
                  multiple: true,
                }}
                variant="outlined"
              >
                {tagAll.map((option) => (
                  <MenuItem key={option.id} value={option.id}>
                    {option.tagName}
                  </MenuItem>
                ))}
              </TextField>
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Category
              </CFormLabel>
              <TextField
                id="categories"
                name="categories"
                select
                style={{ width: "100%" }}
                value={categories}
                onChange={handleChangeCategories}
                SelectProps={{
                  multiple: true,
                }}
                variant="outlined"
              >
                {categoryAll.map((option) => (
                  <MenuItem key={option.id} value={option.id}>
                    {option.categoryName}
                  </MenuItem>
                ))}
              </TextField>
            </Grid>
            <Grid item xs={12}>
              <div style={{ position: "relative", marginBottom: 15 }}>
                <input
                  type="file"
                  accept="image/*"
                  multiple
                  onChange={handleFileChange}
                  style={{
                    opacity: 0,
                    position: "absolute",
                    top: 0,
                    left: 0,
                  }}
                  id="imageInput"
                />
                <label htmlFor="imageInput">
                  <CButton color="primary" className="me-2" size="lg">
                    Choose Images
                  </CButton>
                </label>
              </div>
              <CButton
                color="secondary"
                className="me-2"
                size="lg"
                onClick={handleResetImages}
                style={{ backgroundColor: "red" }}
              >
                Reset Images
              </CButton>

              {selectedImages.map((image, index) => (
                <div key={index}>
                  <Image src={image} alt={`Selected ${index}`} width={80} />
                </div>
              ))}
            </Grid>
          </Grid>
        </Grid>
      </Grid>
      <div className="d-grid gap-2 mt-3">
        <CButton color="primary" onClick={addProduct}>
          Add
        </CButton>
      </div>
    </CContainer>
  </Grid>
</Grid>

    </div>
  );
}
