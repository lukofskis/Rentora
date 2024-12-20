import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import {createHouse, updateHouse } from "../api/houseApi.tsx";
import { toast } from "react-toastify";
import { Link, useNavigate } from "react-router-dom";

// Extend the schema to include the image
const houseSchema = z.object({
  name: z
    .string()
    .min(2, "Name must be at least 2 characters")
    .max(100, "Name must be at most 100 characters"),
  region: z
    .string()
    .min(5, "Description must be at least 5 characters")
    .max(500, "Description must be at most 500 characters"),
  district: z
    .string()
    .min(5, "Address must be at least 5 characters")
    .max(100, "Address must be at most 100 characters"),
});

type HouseInputs = z.infer<typeof houseSchema>;

interface HouseFormProps {
  house?: {
    id: string;
    name: string;
    region: string;
    district: string;
  };
  onClose: () => void;
}

const HouseForm = ({ house, onClose }: HouseFormProps) => {
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    //watch,
    //setError,
  } = useForm<HouseInputs>({
    resolver: zodResolver(houseSchema),
    defaultValues: house
      ? {
          name: house.name,
          region: house.region,
          district: house.district,
        }
      : {
          name: "",
          region: "",
          district: "",
        },
  });

  useEffect(() => {
    if (house) {
      setValue("name", house.name);
      setValue("region", house.region);
      setValue("district", house.district);
    }
  }, [house, setValue]);

  const onSubmit = async (data: HouseInputs) => {
    const formData = new FormData();
    formData.append("name", data.name);
    formData.append("region", data.region);
    formData.append("district", data.district);

    if (house) {
      const response = await updateHouse(house.id, formData);
      if (!response) {
        toast.error("House Update Failed");
      } else {
        navigate(`/house/${house.id}`);
        toast.success("House Updated Successfully");
        onClose();
        navigate(0);
      }
    } else {
      const response = await createHouse(formData);
      if (!response) {
        toast.error("House Creation Failed");
      } else {
        navigate(`/house/${response.id}`);
        toast.success("House Created Successfully");
        onClose();
      }
    }
  };

  return (
    <div className="min-h-screen bg-purple-50 flex justify-center items-center">
      <div className="bg-white p-8 rounded-lg shadow-lg max-w-lg w-full">
        <h2 className="text-3xl font-semibold text-center text-purple-600 mb-6">
          {house ? "Edit house" : "Create a New house"}
        </h2>

        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="mb-6">
            <label
              htmlFor="name"
              className="block text-lg font-medium text-gray-800"
            >
              House name
            </label>
            <input
              id="name"
              type="text"
              {...register("name")}
              className={`w-full p-4 border-2 ${
                errors.name ? "border-red-500" : "border-purple-500"
              } rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-400 transition-all`}
            />
            {errors.name && (
              <p className="text-red-500 text-sm mt-2">{errors.name.message}</p>
            )}
          </div>

          <div className="mb-6">
            <label
              htmlFor="region"
              className="block text-lg font-medium text-gray-800"
            >
              House region
            </label>
            <textarea
              id="region"
              {...register("region")}
              className={`w-full p-4 border-2 min-h-20 ${
                errors.region ? "border-red-500" : "border-purple-500"
              } rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-400 transition-all`}
            />
            {errors.region && (
              <p className="text-red-500 text-sm mt-2">
                {errors.region.message}
              </p>
            )}
          </div>

          <div className="mb-6">
            <label
              htmlFor="district"
              className="block text-lg font-medium text-gray-800"
            >
              House district
            </label>
            <input
              id="district"
              type="text"
              {...register("district")}
              className={`w-full p-4 border-2 ${
                errors.district ? "border-red-500" : "border-purple-500"
              } rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-400 transition-all`}
            />
            {errors.district && (
              <p className="text-red-500 text-sm mt-2">
                {errors.district.message}
              </p>
            )}
          </div>
          <div className="flex justify-center">
            <button
              type="submit"
              className="w-full py-3 px-6 bg-purple-500 text-white rounded-lg hover:bg-purple-600 transition-all duration-300"
            >
              {house ? "Save Changes" : "Create house"}
            </button>
          </div>
        </form>

        {house ? (
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
            <Link to={`/`} className="text-purple-600 hover:text-purple-800">
              Go back
            </Link>
          </div>
        )}
      </div>
    </div>
  );
};

export default HouseForm;
