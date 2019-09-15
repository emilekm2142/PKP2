package yeah.hack.filizanka.controller.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import yeah.hack.filizanka.model.Point;


@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class PointDto {

    private Long pointId;

    private String stationName;

    private Double lng;

    private Double lat;

    public static PointDto from(Point source) {
        return PointDto.builder()
                .pointId(source.getPointId())
                .stationName(source.getStationName())
                .lng(source.getLng())
                .lat(source.getLat())
                .build();
    }
}
