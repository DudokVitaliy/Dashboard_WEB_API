import React from "react";
import { useParams } from "react-router";
import { useGetGameByIdQuery } from "../../store/apis/gameApi";
import { imagesUrl } from "../../env";
import {
  Typography,
  Box,
  Grid,
  Card,
  CardMedia,
  CardContent,
} from "@mui/material";

const GameDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();

  const { data: gameResponse, isLoading, isError } = useGetGameByIdQuery(id!);
  const game = gameResponse?.data ?? null;

  if (isLoading) return <Typography>Завантаження...</Typography>;
  if (isError || !game) return <Typography>Гру не знайдено</Typography>;

  return (
    <Box sx={{ p: 4 }}>
      <Typography variant="h3" gutterBottom sx={{ mb: 2 }}>
        {game.name}
      </Typography>

      <Typography variant="subtitle1" sx={{ mb: 1 }}>
        Розробник: <strong>{game.developer}</strong> | Видавець: <strong>{game.publisher}</strong>
      </Typography>
      <Typography variant="subtitle2" sx={{ mb: 2 }}>
        Дата релізу: {new Date(game.releaseDate).toLocaleDateString()}
      </Typography>

      <Typography variant="h5" sx={{ mb: 3, color: "primary.main" }}>
        Ціна: {game.price}₴
      </Typography>

      <Typography variant="body1" sx={{ mb: 4 }}>
        {game.description}
      </Typography>

      <Card sx={{ mb: 4, maxWidth: 600, mx: "auto", boxShadow: 3 }}>
        <CardMedia
          component="img"
          height="300"
          image={
            game.mainImage
              ? `${imagesUrl}/${game.mainImage.imagePath}`
              : `${imagesUrl}/default.png`
          }
          alt={game.name}
          sx={{ objectFit: "cover" }}
        />
      </Card>

      {game.images?.length > 0 && (
        <Box>
          <Typography variant="h5" gutterBottom sx={{ mb: 2 }}>
            Галерея
          </Typography>
          <Grid container spacing={2}>
            {game.images.map((img) => (
              <Grid size ={4} key={img.id}>
                <Card sx={{ boxShadow: 2 }}>
                  <CardMedia
                    component="img"
                    height="200"
                    image={`${imagesUrl}/${img.imagePath}`}
                    alt={`Image ${img.id}`}
                    sx={{ objectFit: "cover" }}
                  />
                  {img.isMain && (
                    <CardContent sx={{ textAlign: "center", py: 1 }}>
                      <Typography variant="caption" color="primary">
                        Main Image
                      </Typography>
                    </CardContent>
                  )}
                </Card>
              </Grid>
            ))}
          </Grid>
        </Box>
      )}
    </Box>
  );
};

export default GameDetail;
