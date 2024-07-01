import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { Image } from 'react-bootstrap';

function ImageProductEdit({ productId }) {
    const [images, setImages] = useState([]);

    useEffect(() => {
        axios.get(`http://localhost:5284/api/Gallery/by-product/` + productId)
            .then(response => {
                setImages(response.data);
            })
            .catch(error => {
                console.error('Error fetching images:', error);
            });
    }, [productId]);

    // Kiểm tra xem mảng images có phần tử không trước khi truy cập vào thuộc tính 'image'
    if (images.length === 0) {
        return null; // hoặc có thể trả về một phần tử JSX tùy thuộc vào yêu cầu của bạn
    }

    return (
        <div>
            <img src={`data:image/*;base64,${images[0].image}`} style={{ width: 100 }} />
        </div>
    );
}

export default ImageProductEdit;
