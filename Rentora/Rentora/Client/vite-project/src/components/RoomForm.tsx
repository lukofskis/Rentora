import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { createRoom, updateRoom } from "../api/houseApi.tsx";
import { toast } from "react-toastify";
import { Link, useNavigate, useParams } from "react-router-dom";
import NotFoundPage from "../pages/NotFoundPage";

const roomSchema = z.object({
  number: z.coerce
    .number()
    .min(1, "Number must be at least 1")
    .max(9999, "Number must be at most 9999"),
  description: z
      .string()
      .min(5, "Description must be at least 5 characters")
      .max(500, "Description must be at most 500 characters"),
  price: z.coerce
    .number()
    .min(1, "Price must be at least 1")
      .max(99999999, "Price must be at most 99999999"),
 
});

type RoomInputs = z.infer<typeof roomSchema>;

interface RoomFormProps {
  room?: {
    id: string;
    number: number;
    description: string;
    price: number;
    
  } ; //| null; // Pridėkite null kaip galimą reikšmę
  onClose: () => void;
}

const RoomForm = ({ room, onClose }: RoomFormProps) => {
  const navigate = useNavigate();
  const { houseId } = useParams();

  if (!houseId || isNaN(parseInt(houseId))) {
    return <NotFoundPage />;
  }

  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    setError,
  } = useForm<RoomInputs>({
    resolver: zodResolver(roomSchema),
    defaultValues: room
      ? {
          number: room.number,
          description: room.description,
          price: room.price,
          
        }
      : {
          number: 1,
          description: "",
          price: 1,
          
        },
  });

  useEffect(() => {
    if (room) {
      setValue("number", room.number);
      setValue("description", room.description);
      setValue("price", room.price);
      
    }
  }, [room, setValue]);

  const onSubmit = async (data: RoomInputs) => {
    if (room) {
      const response = await updateRoom(houseId, room.id, data);
      if (!response) {
        toast.error("Room Update Failed");
      } else {
        navigate(`/house/${houseId}/room/${room.id}`);
        toast.success("Room Updated Successfully");
        onClose();
        navigate(0);
      }
    } else {
      const response = await createRoom(houseId, data);
      if (!response) {
        toast.error("Room Creation Failed");
      } else {
        if (response === "Room with provided number already exists") {
          setError("root", { message: response });
          toast.error("Room Creation Failed");
        } else {
          navigate(`/house/${houseId}/room/${response.id}`);
          toast.success("Room Created Successfully");
          onClose();
        }
      }
    }
  };

  return (
    <div className="min-h-screen bg-purple-50 flex justify-center items-center">
      <div className="bg-white p-8 rounded-lg shadow-lg max-w-lg w-full">
        <h2 className="text-3xl font-semibold text-center text-purple-600 mb-6">
          {room ? "Edit Room" : "Create a New Room"}
        </h2>

        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="mb-6">
            <label
                htmlFor="number"
                className="block text-lg font-medium text-gray-800"
            >
              Room Number
            </label>
            <input
                id="number"
                type="number"
                min="1"
                {...register("number")}
                className={`w-full p-4 border-2 ${
                    errors.number ? "border-red-500" : "border-purple-500"
                } rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-400 transition-all`}
            />
            {errors.number && (
                <p className="text-red-500 text-sm mt-2">
                  {errors.number.message}
                </p>
            )}
          </div>


          <div className="mb-6">
            <label
                htmlFor="description"
                className="block text-lg font-medium text-gray-800"
            >
              Description
            </label>
            <textarea
                id="description"
                {...register("description")}
                className={`w-full p-4 border-2 min-h-20 ${
                    errors.description ? "border-red-500" : "border-purple-500"
                } rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-400 transition-all`}
            />
            {errors.description && (
                <p className="text-red-500 text-sm mt-2">
                  {errors.description.message}
                </p>
            )}
          </div>

          <div className="mb-6">
            <label
                htmlFor="price"
                className="block text-lg font-medium text-gray-800"
            >
              Price
            </label>
            <input
                id="price"
                type="number"
                min="1"
                {...register("price")}
                className={`w-full p-4 border-2 ${
                    errors.price ? "border-red-500" : "border-purple-500"
                } rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-400 transition-all`}
            />
            {errors.price && (
                <p className="text-red-500 text-sm mt-2">
                  {errors.price.message}
                </p>
            )}
          </div>

          <div className="flex justify-center">
            <button
                type="submit"
                className="w-full py-3 px-6 bg-purple-500 text-white rounded-lg hover:bg-purple-600 transition-all duration-300"
            >
              {room ? "Save Changes" : "Create Room"}
            </button>
          </div>
          {errors.root && (
              <p className="text-red-500 text-sm mt-2">{errors.root.message}</p>
          )}
        </form>
        {room ? (
            <div className="mt-4 text-center">
              <button
                  onClick={onClose}
                  className="text-purple-600 hover:text-purple-800"
              >
                Cancel
              </button>
            </div>
        ) : (
            <div className="mt-4 text-center">
              <Link
                  to={`/house/${houseId}`}
                  className="text-purple-600 hover:text-purple-800"
              >
                Go back
              </Link>
            </div>
        )}
      </div>
    </div>
  );
};

export default RoomForm;
