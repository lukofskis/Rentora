import { House } from "../modules/types";

type Props = {
  house: House;
};

const HouseCard = ({ house }: Props) => {
  return (
    <div className="max-w-sm rounded-lg overflow-hidden shadow-lg bg-white">
      <div className="p-4">
        <h3 className="text-xl font-semibold text-gray-800">{house.name}</h3>
        <p className="text-gray-600 text-sm">{house.region}</p>
        <p className="text-gray-700 mt-2">{house.district}</p>
      </div>
    </div>
  );
};

export default HouseCard;
