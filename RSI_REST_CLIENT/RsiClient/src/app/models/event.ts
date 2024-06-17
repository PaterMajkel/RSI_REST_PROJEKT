import { Details } from "./details";
import { Link } from "./link";

export interface EventDt {
  id: number;
  name: string;
  details: Details;
  links: Link[];
}
