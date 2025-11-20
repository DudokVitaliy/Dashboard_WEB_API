import {
    Box,
    Button,
    FormLabel,
    styled,
    TextField,
    Typography,
} from "@mui/material";
import { useFormik } from "formik";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import type { CreateForm } from "./types";
import { useState, type ChangeEvent } from "react";
import { useCreateGameMutation } from "../../store/apis/gameApi";
import { useNavigate } from "react-router";

const FieldBox = styled(Box)(() => ({
    display: "flex",
    flexDirection: "column",
    alignItems: "left",
    width: "100%",
}));

const VisuallyHiddenInput = styled("input")({
    clip: "rect(0 0 0 0)",
    clipPath: "inset(50%)",
    height: 1,
    overflow: "hidden",
    position: "absolute",
    bottom: 0,
    left: 0,
    whiteSpace: "nowrap",
    width: 1,
});

const CreateGamePage = () => {
    const [mainImage, setMainImage] = useState<File | null>(null);
    const [images, setImages] = useState<File[]>([]);

    const [createGame] = useCreateGameMutation();
    const navigate = useNavigate();

    const initValues: CreateForm = {
        name: "",
        description: "",
        price: 0,
        publisher: "",
        developer: "",
        releaseDate: new Date().toISOString().substring(0, 10),
    };

    const handleSubmit = async (values: CreateForm) => {
        if(!mainImage) {
            return;
        }

        const formData = new FormData();
        formData.append("name", values.name);
        formData.append("description", values.description);
        formData.append("developer", values.developer);
        formData.append("publisher", values.publisher);
        formData.append("releaseDate", values.releaseDate);
        formData.append("price", values.price.toString());
        formData.append("mainImage", mainImage);
        for(const image of images) {
            formData.append("images", image);
        }

        try {
            const response = await createGame(formData).unwrap();
            if(response.isSuccess) {
                navigate("/game", { replace: true })
            }   
        } catch (error) {
            console.log(error);
        }
        
    };

    const handleMainImageChange = (event: ChangeEvent<HTMLInputElement>) => {
        if (event.target.files && event.target.files?.length > 0) {
            setMainImage(event.target.files[0]);
        }
    };

    const handleImagesChange = (event: ChangeEvent<HTMLInputElement>) => {
        const files = event.target.files;
        if (files) {
            for (let i = 0; i < files.length; i++) {
                setImages((prev) => [...prev, files[i]]);
            }
        }
    };

    const formik = useFormik<CreateForm>({
        initialValues: initValues,
        onSubmit: handleSubmit,
    });

    return (
        <Box
            component="form"
            mt={2}
            display="flex"
            flexDirection="column"
            alignItems="center"
            width="40%"
            mx="auto"
            onSubmit={formik.handleSubmit}
        >
            <Typography variant="h4" fontWeight="bold">
                Додавання нової гри
            </Typography>
            {/* Name */}
            <FieldBox my={1} px={2}>
                <FormLabel htmlFor="name">Назва</FormLabel>
                <TextField
                    name="name"
                    id="name"
                    placeholder="Назва гри"
                    type="text"
                    variant="outlined"
                    onChange={formik.handleChange}
                    value={formik.values.name}
                />
            </FieldBox>
            {/* Description */}
            <FieldBox my={1} px={2}>
                <FormLabel htmlFor="description">Опис</FormLabel>
                <TextField
                    name="description"
                    id="description"
                    placeholder="Опис"
                    type="text"
                    variant="outlined"
                    onChange={formik.handleChange}
                    value={formik.values.description}
                />
            </FieldBox>
            {/* Price */}
            <FieldBox my={1} px={2}>
                <FormLabel htmlFor="price">Ціна</FormLabel>
                <TextField
                    name="price"
                    id="price"
                    placeholder="Ціна"
                    type="number"
                    variant="outlined"
                    onChange={formik.handleChange}
                    value={formik.values.price}
                />
            </FieldBox>
            {/* ReleaseDate */}
            <FieldBox my={1} px={2}>
                <FormLabel htmlFor="releaseDate">Дата виходу</FormLabel>
                <TextField
                    name="releaseDate"
                    id="releaseDate"
                    placeholder="Дата виходу"
                    type="date"
                    variant="outlined"
                    onChange={formik.handleChange}
                    value={formik.values.releaseDate}
                />
            </FieldBox>
            {/* Publisher */}
            <FieldBox my={1} px={2}>
                <FormLabel htmlFor="publisher">Видавець</FormLabel>
                <TextField
                    name="publisher"
                    id="publisher"
                    placeholder="Видавець"
                    type="text"
                    variant="outlined"
                    onChange={formik.handleChange}
                    value={formik.values.publisher}
                />
            </FieldBox>
            {/* Developer */}
            <FieldBox my={1} px={2}>
                <FormLabel htmlFor="developer">Розробник</FormLabel>
                <TextField
                    name="developer"
                    id="developer"
                    placeholder="Розробник"
                    type="text"
                    variant="outlined"
                    onChange={formik.handleChange}
                    value={formik.values.developer}
                />
            </FieldBox>
            {/* MainImage */}
            {mainImage && (
                <Box>
                    <Box
                        component="img"
                        src={URL.createObjectURL(mainImage!)}
                        alt="Cover"
                        width={300}
                        height={150}
                    ></Box>
                </Box>
            )}
            <FieldBox my={1} px={2}>
                <Button
                    component="label"
                    role={undefined}
                    variant="contained"
                    color="secondary"
                    tabIndex={-1}
                    startIcon={<CloudUploadIcon />}
                >
                    Завантажити обкладинку
                    <VisuallyHiddenInput
                        accept="image/*"
                        type="file"
                        onChange={handleMainImageChange}
                    />
                </Button>
            </FieldBox>
            {/* Images */}
            {images.length > 0 && (
                <Box>
                    {images.map((image, index) => (
                        <Box
                            key={index}
                            component="img"
                            src={URL.createObjectURL(image!)}
                            alt="Cover"
                            width={150}
                            height={75}
                        ></Box>
                    ))}
                </Box>
            )}
            <FieldBox my={1} px={2}>
                <Button
                    component="label"
                    role={undefined}
                    variant="contained"
                    color="warning"
                    tabIndex={-1}
                    startIcon={<CloudUploadIcon />}
                >
                    Завантажити зображення гри
                    <VisuallyHiddenInput
                        accept="image/*"
                        type="file"
                        multiple
                        onChange={handleImagesChange}
                    />
                </Button>
            </FieldBox>
            <Box my={1} px={2}>
                <Button color="primary" variant="contained" type="submit">
                    Створити
                </Button>
            </Box>
        </Box>
    );
};

export default CreateGamePage;