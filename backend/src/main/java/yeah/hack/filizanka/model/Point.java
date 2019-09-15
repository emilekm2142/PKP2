package yeah.hack.filizanka.model;


import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
@AllArgsConstructor
@NoArgsConstructor
@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class Point {

    @Id
    @GeneratedValue
    private Long pointId;

    @JsonProperty(value="city")
    private String stationName;

    @JsonProperty(value="longitude")
    private Double lng;

    @JsonProperty(value="latitude")
    private Double lat;

    private boolean station = true;
}
