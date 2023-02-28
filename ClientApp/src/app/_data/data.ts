import { selectOption } from "../models/model";

export const sortByList:Array<selectOption> = [
  {value: "id", displayValue: "Id" },
  {value: "reads", displayValue: "Reads" },
  {value: "likes", displayValue: "Likes" },
  {value: "popularity", displayValue: "Popularity" },
];

export const directionList:Array<selectOption> = [
  {value: "asc", displayValue: "Ascending" },
  {value: "desc", displayValue: "Descending" },
];
